using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Factories;
using HomeFinanceApp.Models.Finance;
using HomeFinanceApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Models
{
    internal class FamilyMember
    {
        private static int MemberId = 0;
        private decimal CurrentlyMoney { get; set; } //Сколько сейчас денег
        private decimal MonthlyIncome { get; set; } //Сколько в этом месяце дали денег
        private decimal LastMonthIncome { get; set; } //Сколько в прошлом месяце дали денег

        public int Id { get; private set; }
        public string Name { get; set; }


        private readonly Family _family;
        private readonly Random _rand = new Random();
        private readonly ManualResetEvent _startNewMonth;
        private readonly ManualResetEvent _waitDistributionMoney;
        private readonly Thread _thread;

        public MemberRole memberRole { get; set; }
        public List<Credit> credits { get; set; } = new();// кредиты члена
        public List<Expense> expenses { get; set; } = new();// потребности
        public List<Income> incomes { get; set; } = new();
        public Stat stat { get; set; } = new();
        

        
        public FamilyMember(string Name, MemberRole memberRole, Family family, 
            ManualResetEvent startNewMonth, ManualResetEvent waitDistributionMoney
            )
        {
            Id = MemberId++;
            this.Name = Name;
            this.memberRole = memberRole;
            _family = family;
            _startNewMonth = startNewMonth;
            _waitDistributionMoney = waitDistributionMoney;
            _thread = new Thread(Life);
            _thread.Start();
        }
        private void Life()
        {
            while (true)
            {
                _startNewMonth.WaitOne(); //Ждём пока начнётся новый месяц
                
                stat.Clear();

                Thread.Sleep(1000);

                GiveMoneyToFamily(); // Отдаём все деньги семье

                _waitDistributionMoney.WaitOne(); // Ждём распределения денег

                RepayLoans(); // Оплачиваем кредиты

                AddOrRemoveExpenses(); // Обновляем свои потребности

                WasteMoneyOnExpenses(); // Тратим деньги на свои потребности

                UpdateMoneyIncomes(); // убираем лотерею и подарки из доходов, а также обновляем остальные

                TryAddNewMoneyIncomes(); // пытаемся добавить новый доход

            }
        }
        public void AddExtraMoney(decimal money)
        {
            MonthlyIncome += money;
            CurrentlyMoney += money;
            _family.Notify(FamilyEvents.MemberNeedExtraMoneyFromMoneyBox, Id, money);
        }
        public void AddMonthMoney(decimal money)
        {
            MonthlyIncome += money;
            CurrentlyMoney += money;
            _family.Notify(FamilyEvents.MemberGetMoney, Id, money);
        }
        private void UpdateMoneyIncomes()
        {
            incomes.RemoveAll(x => x.IncomeType == IncomeTypes.Gift || x.IncomeType == IncomeTypes.Lottery);

            LastMonthIncome = MonthlyIncome;
            MonthlyIncome = 0;
            
            foreach (var x in incomes)
            {
                int chance = (_rand.Next(0, 2) == 1) ? 1 : -1; 
                //прибавляем к заработку 10% или убираем 10%,  с шансом 50%
                x.IncomeAmount += x.IncomeAmount * 0.1m * chance;
            }
        }
        private void TryAddNewMoneyIncomes()
        {
            int chance = (memberRole == MemberRole.Mother || memberRole == MemberRole.Father) ? 80 : 10;

            if (_rand.Next(0, 100) < chance)
            {
                Income inc = FinanceFactory.CreateRandomIncome();

                incomes.Add(inc);
                stat.incomeMoneyToFamily.Add(inc);
            }
        }
        private void GiveMoneyToFamily()
        {
            decimal summa = 0;
            foreach (var x in incomes)
            {
                summa += x.IncomeAmount;
                stat.incomeMoneyToFamily.Add(x);
            }
            if (summa > 0)
                _family.InputMoneyToFamily(Id, summa);

            _family.SignalMoneyContributed();
        }
        private void AddOrRemoveExpenses()
        {
            int chance = (MonthlyIncome > LastMonthIncome) ? 70 : 30;

            if (true)
                expenses.Add(FinanceFactory.CreateNewRandomExpense(expenses));
            else if (expenses.Count > 1) 
                expenses.RemoveAt(expenses.Count - 1);

        }
        private void RepayLoans()
        {
            if (credits.Any())
            {
                decimal moneyToSavings = 0;
                decimal moneyOnCredits = CurrentlyMoney * 0.25m;
                decimal moneyOnEveryCredits = moneyOnCredits / credits.Count;

                foreach (var credit in credits)
                {
                    var remainsMoney = credit.MakePayment(moneyOnEveryCredits);

                    stat.wasteMoneyOnCredits.Add((credit, moneyOnEveryCredits - remainsMoney));

                    moneyToSavings += remainsMoney;
                }

                credits.RemoveAll(x => x.RemainingAmount == 0);

                _family.AddMoneyToSavings(moneyToSavings, Id);
                
                CurrentlyMoney *= 0.75m;
            }
        }
        private void WasteMoneyOnExpenses()
        {
            foreach (var expense in expenses)
            {
                decimal money = _family.pricesOfExpensesOnMonth[expense.ExpenseSubTypes];

                if (CurrentlyMoney >= money)
                {
                    CurrentlyMoney -= money;
                    stat.wasteMoneyOnExpenses.Add((expense, money));
                }else
                {
                    //с шансом 50% берём кредит на потребность
                    if (_rand.Next(0, 10) < 5)
                    {
                        credits.Add(FinanceFactory.CreateCredit(expense, money - CurrentlyMoney));
                        CurrentlyMoney = 0;
                        break;
                    }
                    if (CurrentlyMoney != 0) //либо кладём оставшиеся деньги в сберегательный фонд
                    {
                        _family.AddMoneyToSavings(CurrentlyMoney, Id);
                        CurrentlyMoney = 0;
                        break;
                    }
                }
            }

            //Если мы удовлетворили все свои потребности, то остальное кладём в сбережения
            if (CurrentlyMoney > 0)
            {
                _family.AddMoneyToSavings(CurrentlyMoney, Id);
                CurrentlyMoney = 0;
            }

        }
    }
}
