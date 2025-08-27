using HomeFinanceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Views.Forms
{
    internal class RoleVisual
    {
        public int roleId;
        public Point non_visible_position;
        public Point position_near_to_table;
        public Point position_of_money;
        public Image image;

        public RoleVisual(int roleId, Point non_visible_position,
            Point position_near_to_table, Point position_of_money, byte[] image)
        {
            this.roleId = roleId;
            this.non_visible_position = non_visible_position;
            this.position_near_to_table = position_near_to_table;
            this.position_of_money = position_of_money;
            this.image = ConvertHelper.ByteArrayToImage(image);
        }
    }
}
