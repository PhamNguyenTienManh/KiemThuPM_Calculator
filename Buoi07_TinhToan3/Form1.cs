using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;   // thêm để dùng BigInteger
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Buoi07_TinhToan3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSo1.Text = txtSo2.Text = "0";
            radCong.Checked = true;             //đầu tiên chọn phép cộng
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có thực sự muốn thoát không?",
                                 "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            // lấy giá trị của 2 ô số dưới dạng BigInteger
            BigInteger so1, so2;
            if (!BigInteger.TryParse(txtSo1.Text, out so1) || !BigInteger.TryParse(txtSo2.Text, out so2))
            {
                MessageBox.Show("Chỉ được nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string kq = "";

            // Thực hiện phép toán dựa vào phép toán được chọn
            if (radCong.Checked) kq = (so1 + so2).ToString();
            else if (radTru.Checked) kq = (so1 - so2).ToString();
            else if (radNhan.Checked) kq = (so1 * so2).ToString();
            else if (radChia.Checked)
            {
                if (so2 == 0)
                {
                    MessageBox.Show("Không thể chia cho 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSo2.Focus();
                    txtSo2.SelectAll();
                    return;
                }
                else
                {
                    // Nếu chia hết thì in số nguyên
                    if (so1 % so2 == 0)
                    {
                        kq = (so1 / so2).ToString();
                    }
                    else
                    {
                        // chuyển sang double để ra số thập phân
                        double thuong = (double)so1 / (double)so2;
                        kq = thuong.ToString("0.##########"); // hiển thị tối đa 10 số sau dấu phẩy
                    }
                }
            }

            // Hiển thị kết quả lên trên ô kết quả
            txtKq.Text = kq;
        }

        private void txtSo1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSo1_MouseClick(object sender, MouseEventArgs e)
        {
            txtSo1.SelectAll();
        }

        private void txtSo1_Click(object sender, EventArgs e)
        {

        }

        private void txtSo2_MouseClick(object sender, MouseEventArgs e)
        {
            txtSo2.SelectAll();
        }

        private void txtSo1_Leave(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                MessageBox.Show("Không được để trống ô này");
                tb.Focus();
                return;
            }
            ValidateNumberInput(txtSo1);
        }

        private void txtSo2_Leave(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                MessageBox.Show("Không được để trống ô này");
                tb.Focus();
                return;
            }
            ValidateNumberInput(txtSo2);
        }

        private void ValidateNumberInput(System.Windows.Forms.TextBox tb)
        {
            // Nếu không phải số nguyên hợp lệ
            BigInteger temp;
            if (!BigInteger.TryParse(tb.Text, out temp))
            {
                MessageBox.Show("Chỉ được nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Focus();
                tb.SelectAll();
            }
        }
    }
}
