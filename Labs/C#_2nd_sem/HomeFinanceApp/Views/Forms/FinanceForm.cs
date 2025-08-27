using HomeFinanceApp.Controllers;
using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Core.Interfaces;
using HomeFinanceApp.Models;
using HomeFinanceApp.Services;
using HomeFinanceApp.Views.Forms;
using HomeFinanceApp.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeFinanceApp.Views.Forms
{
    internal partial class FinanceForm : Form, IFinanceView
    {
        #region Fields and Properties
        public FinanceController financeController;

        private Dictionary<int, PictureBox> _membersPictures = new();
        private Dictionary<int, PictureBox> _membersMoneyPictures = new();
        private Dictionary<int, int> _idToRole = new();
        private Point sizeOfMoney = new Point(72, 34);
        private Image moneyImage = ConvertHelper.ByteArrayToImage(Properties.Resources.money);

        private List<PictureBox> _moneyPictures = new();
        private List<RoleVisual> _roleVisuals = new()
        {
            new RoleVisual(0, new Point(424, -230), new Point(424, -1), new Point(436, 99), Properties.Resources.son),
            new RoleVisual(1, new Point(967, 109), new Point(795, 109), new Point(706, 191), Properties.Resources.daughter),
            new RoleVisual(2, new Point(381, 518), new Point(381, 249), new Point(524, 260), Properties.Resources.mother),
            new RoleVisual(3, new Point(-200, 155), new Point(10, 155), new Point(149, 194), Properties.Resources.father),
        };
        #endregion

        public FinanceForm()
        {
            InitializeComponent();
            _moneyPictures = new()
            {
                memberMoney1, memberMoney2, memberMoney3, memberMoney4
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            financeController.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ShowFamilyStatistics();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ShowMemberStatistics(_idToRole.First(x => x.Value == comboBox1.SelectedIndex).Key);
        }

        #region Animation Methods
        private async Task AnimateMoneyAsync(PictureBox pb, Point targetPos)
        {
            Point startPos = pb.Location;
            int steps = 30;

            for (int i = 1; i <= steps; i++)
            {
                float ratio = (float)i / steps;
                ratio = (float)(1 - Math.Pow(1 - ratio, 2));

                Point newPos = new Point(
                    startPos.X + (int)((targetPos.X - startPos.X) * ratio),
                    startPos.Y + (int)((targetPos.Y - startPos.Y) * ratio)
                );

                pb.Invoke((MethodInvoker)(() => pb.Location = newPos));
                await Task.Delay(16);
            }

            pb.Invoke((MethodInvoker)(() => pb.Location = targetPos));
        }
        private async Task MovePictureBox(PictureBox pictureBox, Point targetPosition, int durationMs = 300)
        {
            if (pictureBox.Image == moneyImage)
                Console.WriteLine($"Moving from {pictureBox.Location} to {targetPosition}");
            Point startPosition = pictureBox.Location;
            float distanceX = targetPosition.X - startPosition.X;
            float distanceY = targetPosition.Y - startPosition.Y;
            int steps = Math.Max(1, durationMs / 16);

            float stepX = distanceX / steps;
            float stepY = distanceY / steps;

            for (int i = 1; i <= steps; i++)
            {
                int newX = startPosition.X + (int)(stepX * i);
                int newY = startPosition.Y + (int)(stepY * i);

                this.InvokeIfRequired(() =>
                {
                    pictureBox.Left = newX;
                    pictureBox.Top = newY;
                });

                await Task.Delay(16);
            }

            this.InvokeIfRequired(() =>
            {
                pictureBox.Left = targetPosition.X;
                pictureBox.Top = targetPosition.Y;
            });
        }
        #endregion

        #region PictureBox Creation Methods
        private PictureBox CreateMemberDependOnRole(int roleId)
        {
            var member = new PictureBox();
            member.SizeMode = PictureBoxSizeMode.StretchImage;
            member.Size = new Size(137, 219);
            member.BackColor = Color.Transparent;

            member.Image = _roleVisuals.First(x => x.roleId == roleId).image;
            member.Location = _roleVisuals.First(x => x.roleId == roleId).non_visible_position;

            this.InvokeIfRequired(() =>
            {
                Controls.Add(member);
                if (roleId == 2)
                    member.BringToFront();
            });
            return member;
        }
        #endregion

        #region Methods for Statistics
        public void ShowMemberStatistics(int memberId)
        {
            var stat = financeController.GetStatByRoleId(_idToRole[memberId]);
            string title = $"Статистика {_membersPictures[memberId].Tag}";
            var statsForm = new FinanceStatsForm(stat, title);
            statsForm.Show();
        }

        public void ShowFamilyStatistics()
        {
            var stats = financeController.GetStatOfAllMembers();
            var combinedStat = CombineStats(stats);
            var statsForm = new FinanceStatsForm(combinedStat, "Общая статистика семьи");
            statsForm.Show();
        }

        private Stat CombineStats(List<Stat> stats)
        {
            var combined = new Stat();

            foreach (var stat in stats)
            {
                combined.wasteMoneyOnCredits.AddRange(stat.wasteMoneyOnCredits);
                combined.wasteMoneyOnExpenses.AddRange(stat.wasteMoneyOnExpenses);
                combined.incomeMoneyToFamily.AddRange(stat.incomeMoneyToFamily);
            }

            return combined;
        }
        #endregion

        #region IFinanceView Implementation
        public void CreateMember(int memberId, int roleId)
        {
            _idToRole[memberId] = roleId;
            _membersPictures[memberId] = CreateMemberDependOnRole(roleId);


            this.InvokeIfRequired(() =>
            {
                _moneyPictures[memberId].Visible = false;
                _moneyPictures[memberId].Location = _roleVisuals.First(x => x.roleId == roleId).position_of_money;
                _membersMoneyPictures[memberId] = _moneyPictures[memberId];
            });
        }

        public async void MemberGetMoney(int memberId, decimal money)
        {
            var moneyPic = _membersMoneyPictures[memberId];
            moneyPic.Invoke((MethodInvoker)(() =>
            {
                moneyPic.Location = moneysPicture.Location;
                moneyPic.Visible = true;
                moneyPic.BringToFront();
            }));

            var targetPos = _roleVisuals.First(x => x.roleId == _idToRole[memberId]).position_of_money;
            await Task.Run(() => AnimateMoneyAsync(moneyPic, targetPos));

            moneyPic.Invoke((MethodInvoker)(() => moneyPic.Visible = false));
        }

        public async void MemberInputMoneyToSavings(int memberId, decimal money)
        {
            var moneyPic = _membersMoneyPictures[memberId];
            moneyPic.Invoke((MethodInvoker)(() => moneyPic.Visible = true));

            await AnimateMoneyAsync(moneyPic, savingsPicture.Location);

            moneyPic.Invoke((MethodInvoker)(() => moneyPic.Visible = false));
        }

        public async void MemberNeedExtraMoneyFromMoneyBox(int memberId, decimal money)
        {
            var moneyPic = _membersMoneyPictures[memberId];
            moneyPic.Invoke((MethodInvoker)(() =>
            {
                moneyPic.Location = savingsPicture.Location;
                moneyPic.Visible = true;
            }));

            var targetPos = _roleVisuals.First(x => x.roleId == _idToRole[memberId]).position_of_money;
            await AnimateMoneyAsync(moneyPic, targetPos);
        }

        public async void MemberDropMoney(int memberId, decimal money)
        {
            var moneyPic = _membersMoneyPictures[memberId];
            _membersMoneyPictures[memberId].Invoke((MethodInvoker)(() => _membersMoneyPictures[memberId].Visible = true));

            await AnimateMoneyAsync(_membersMoneyPictures[memberId], moneysPicture.Location);

            _membersMoneyPictures[memberId].Invoke((MethodInvoker)(() => _membersMoneyPictures[memberId].Visible = false));
        }

        public async void FamilyPostponedMoney(decimal money)
        {
            // Используем временную картинку для общего перемещения
            var temp = MovementAnimation.CreatePicture(this, moneyImage, (Size)sizeOfMoney, moneysPicture.Location);
            await Task.Run(() => AnimateMoneyAsync(temp, savingsPicture.Location));
        }


        public async void StartNewMonth()
        {
            var moveTasks = new List<Task>();
            foreach (var member in _membersPictures)
            {
                var task = Task.Run(() =>
                {
                    this.InvokeIfRequired(() =>
                    {
                        _membersMoneyPictures[member.Key].Visible = false;
                        _membersMoneyPictures[member.Key].Location = _roleVisuals.First(x => x.roleId == _idToRole[member.Key]).position_of_money;
                    });
                    return MovePictureBox(member.Value, _roleVisuals.First(x => x.roleId == _idToRole[member.Key]).position_near_to_table);
                });
                moveTasks.Add(task);
            }
            await Task.WhenAll(moveTasks);
        }
        public async void GatherEnded()
        {
            var moveTasks = new List<Task>();
            foreach (var member in _membersPictures)
            {

                var task = Task.Run(() =>
                {

                    return MovePictureBox(member.Value, _roleVisuals.First(x => x.roleId == _idToRole[member.Key]).non_visible_position);
                });
                moveTasks.Add(task);
            }

            await Task.WhenAll(moveTasks);
        }

        public void SavingsChanged(decimal money)
        {
            this.InvokeIfRequired(() => savingsLabel.Text = money.ToString());
        }

        public void AmountChanged(decimal money)
        {
            this.InvokeIfRequired(() =>
            {
                bool shouldShow = money != 0;
                moneysLabel.Visible = shouldShow;
                moneysPicture.Visible = shouldShow;
                moneysLabel.Text = money.ToString();
            });
        }




        #endregion

        #region IObserver Implementation
        void IObserver.OnFamilyEvent(FamilyEvents eventType, int memberId, decimal money)
        {
            // Все обработчики событий уже вызываются через InvokeIfRequired в своих методах
            switch (eventType)
            {
                case FamilyEvents.CreateMember:
                    CreateMember(memberId, (int)money);
                    break;
                case FamilyEvents.AmountValueChanged:
                    AmountChanged(money);
                    break;
                case FamilyEvents.SavingsValueChanged:
                    SavingsChanged(money);
                    break;
                case FamilyEvents.MemberGetMoney:
                    MemberGetMoney(memberId, money);
                    break;
                case FamilyEvents.MemberDropMoney:
                    MemberDropMoney(memberId, money);
                    break;
                case FamilyEvents.MemberInputMoneyToSavings:
                    MemberInputMoneyToSavings(memberId, money);
                    break;
                case FamilyEvents.MemberNeedExtraMoneyFromMoneyBox:
                    MemberNeedExtraMoneyFromMoneyBox(memberId, money);
                    break;
                case FamilyEvents.FamilyPostponedMoney:
                    FamilyPostponedMoney(money);
                    break;
                case FamilyEvents.StartNewMonth:
                    StartNewMonth();
                    break;
                case FamilyEvents.GatherEnded:
                    GatherEnded();
                    break;
            }
        }
        #endregion
    }
}