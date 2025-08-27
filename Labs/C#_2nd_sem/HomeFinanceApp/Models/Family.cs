using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Core.Interfaces;
using HomeFinanceApp.Factories;
using HomeFinanceApp.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Models
{
    internal class Family : IObservable
    {
        private static object _savingsLock = new();
        private static object _observableLock = new();
        private static object _amountLock = new();
        private Thread _thread;
        private readonly ManualResetEvent _monthStartEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _waitDistibutionEvent = new ManualResetEvent(false);
        private readonly CountdownEvent _moneyContributionEvent = new CountdownEvent(4);

        public decimal Savings { get; set; }
        public decimal TotalAmount { get; set; }

        private List<IObserver> observers = new();
        public Dictionary<ExpenseSubTypes, decimal> pricesOfExpensesOnMonth = new();
        public List<FamilyMember> _familyMembers = new();

        public Family() {
            _thread = new Thread(StartNewMonth);
        }
        #region Family Setting
        private void AddMember(string name, MemberRole role, Family family,
            ManualResetEvent m1, ManualResetEvent m2)
        {
            var fm = MemberFactory.CreateFamilyMember(name, role, family, m1, m2);

            _familyMembers.Add(fm);
            Notify(FamilyEvents.CreateMember, memberId: fm.Id, summa: (int)role);
        }
        private void AddMembers()
        {
            AddMember("Отец", MemberRole.Father, this, _monthStartEvent, _waitDistibutionEvent);
            AddMember("Мать", MemberRole.Mother, this, _monthStartEvent, _waitDistibutionEvent);
            AddMember("Сын", MemberRole.Son, this, _monthStartEvent, _waitDistibutionEvent);
            AddMember("Дочь", MemberRole.Daughter, this, _monthStartEvent, _waitDistibutionEvent);
        }
        private void StartNewMonth()
        {
            AddMembers();
            while (true)
            {
                pricesOfExpensesOnMonth = ExpenseCalculator.CalculateExpensePrices();
                _moneyContributionEvent.Reset(4);

                Notify(FamilyEvents.StartNewMonth);

                _monthStartEvent.Set(); // Запускаем работу семьи

                _moneyContributionEvent.Wait();//ВОТ ТУТ НАДО ДОЖДАТЬСЯ ПОКА ЧЛЕНЫ СЕМЬИ ВНЕСУТ ДЕНЬГИ

                DistributeMoney();
                Thread.Sleep(2500); // Ждём распределения денег 

                _monthStartEvent.Reset(); //Снова поднимаем барьер 
                _waitDistibutionEvent.Set(); // Запускаем работу семьи после распределения денег

                Thread.Sleep(5000);
                Notify(FamilyEvents.GatherEnded);
                Thread.Sleep(11000); // Ждём пока члены семьи разойдутся и следующей итерации
                _waitDistibutionEvent.Reset(); // Снова поднимаем барьер 
            }
        }
        public void StartSimulation()
        {
            _thread.Start();
        }
        public void SignalMoneyContributed()
        {
            _moneyContributionEvent.Signal();
        }
        #endregion

        #region Family Finance Manipulation
        private void DistributeMoney()
        {
            AddMoneyToAmount(-1m * TotalAmount * 0.1m);
            Thread.Sleep(1000);

            AddMoneyToSavings(TotalAmount * 0.1m);

            Thread.Sleep(1000);

            int cnt = _familyMembers.Where(x => x.credits.Any()).Count();

            foreach (var member in _familyMembers)
            {
                decimal multi = (member.memberRole == MemberRole.Mother || member.memberRole == MemberRole.Father) ? 0.35m : 0.15m;
                member.AddMonthMoney(TotalAmount * multi);
                if (member.credits.Any())
                    member.AddExtraMoney(Savings * 0.15m);
            }

            AddMoneyToAmount(-TotalAmount);


            // Из сбережений даём каждому кто имеет кредит по 15% из накоплений
            TakeMoneyFromSavings(Savings * cnt * 0.15m);
        }
        public void AddMoneyToSavings(decimal moneyToSavings, int memberId = -1)
        {
            lock (_savingsLock)
            {
                Savings += moneyToSavings;
                Notify(FamilyEvents.SavingsValueChanged, summa: Savings);
            }

            if (memberId != -1)
                Notify(FamilyEvents.MemberInputMoneyToSavings, memberId, moneyToSavings);
            else
                Notify(FamilyEvents.FamilyPostponedMoney, summa: moneyToSavings);
        }

        public void AddMoneyToAmount(decimal money)
        {
            lock (_amountLock)
            {
                TotalAmount += money;
                Notify(FamilyEvents.AmountValueChanged, summa: TotalAmount);
            }
        }
        public void TakeMoneyFromSavings(decimal money)
        {
            lock (_savingsLock)
            {
                Savings -= money;
                Notify(FamilyEvents.SavingsValueChanged, summa: Savings);

            }
        }
        public void InputMoneyToFamily(int memberId, decimal summa)
        {
            AddMoneyToAmount(summa);
            Notify(FamilyEvents.MemberDropMoney, memberId, summa);
        }
        #endregion

        #region Notifications System
        public void Notify(FamilyEvents familyEvents, int memberId = 0, decimal summa = 0)
        {
            lock (_observableLock)
            {
                foreach(var observer in observers)
                    observer.OnFamilyEvent(familyEvents, memberId, summa);
            }
        }

        public void Subscribe(IObserver observer)
        {
            lock (_observableLock)
            {
                observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer)
        {
            lock (_observableLock)
            {
                observers.Remove(observer);
            }
        }
        #endregion

    }
}
