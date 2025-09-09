using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;   // để dùng BigInteger

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
            radCong.Checked = true;             // đầu tiên chọn phép cộng
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
            try
            {
                // Lấy giá trị của 2 ô số (dùng BigInteger để tính số cực lớn)
                BigInteger so1 = BigInteger.Parse(txtSo1.Text);
                BigInteger so2 = BigInteger.Parse(txtSo2.Text);
                BigInteger kq = 0;

                // Thực hiện phép toán dựa vào phép toán được chọn
                if (radCong.Checked) kq = so1 + so2;
                else if (radTru.Checked) kq = so1 - so2;
                else if (radNhan.Checked) kq = so1 * so2;
                else if (radChia.Checked)
                {
                    if (so2 != 0)
                    {
                        // gọi hàm chia có thập phân (20 chữ số sau dấu phẩy)
                        txtKq.Text = ChiaCoThapPhan(so1, so2, 20);
                    }
                    else
                    {
                        MessageBox.Show("Không thể chia cho 0");
                        return;
                    }
                    return; // kết thúc sớm vì đã gán txtKq
                }

                // Hiển thị kết quả (với cộng, trừ, nhân)
                txtKq.Text = kq.ToString();
            }
            catch
            {
                MessageBox.Show("Giá trị nhập vào không hợp lệ!");
            }
        }

        // Hàm chia có phần thập phân (mặc định lấy 20 chữ số sau dấu phẩy)
        private string ChiaCoThapPhan(BigInteger so1, BigInteger so2, int soChuSoSauDauPhay = 20)
        {
            if (so2.IsZero) return "Không thể chia cho 0";

            BigInteger phanNguyen = so1 / so2;
            BigInteger du = so1 % so2;

            if (du.IsZero) return phanNguyen.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append(phanNguyen);
            sb.Append(".");

            for (int i = 0; i < soChuSoSauDauPhay; i++)
            {
                du *= 10;
                BigInteger so = du / so2;
                sb.Append(so);
                du %= so2;
                if (du.IsZero) break;
            }

            return sb.ToString();
        }

        private void txtSo1_TextChanged(object sender, EventArgs e)
        {
            // Bạn có thể xử lý gì đó khi text thay đổi, tạm thời để trống
        }

        private void txtSo1_MouseClick(object sender, MouseEventArgs e)
        {
            txtSo1.SelectAll();   // click chuột vào thì chọn hết số
        }

        private void txtSo1_Click(object sender, EventArgs e)
        {
            // Có thể xử lý thêm nếu cần, hiện tại để trống
        }

        private void txtSo2_MouseClick(object sender, MouseEventArgs e)
        {
            txtSo2.SelectAll();   // click chuột vào thì chọn hết số
        }

        private void txtSo1_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                MessageBox.Show("Không được để trống ô này");
                tb.Focus();
            }
        }

        private void txtSo2_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                MessageBox.Show("Không được để trống ô này");
                tb.Focus();
            }
        }
    }
}
