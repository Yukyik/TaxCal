using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public double sum_group1, sum_group2, sum_group3, sum_group4;
        double Salary_allyear = 0;
        double temp_100000;
        public Form1()
        {
            InitializeComponent();
            child_before_61.Enabled = false;
            child_after_61.Enabled = false;
            HealthcareTextBox.Enabled = false;

            Firsthouse_ComboBox.Enabled = false;
            Firsthouse_money.Enabled = false;
        }
        private void button_Click(object sender, EventArgs e)
        {
            double tax = 0;
            double Sum_all = sum_group1 + sum_group2 + sum_group3 + sum_group4;
            double Reduced = Salary_allyear - Sum_all;
            if (sender == Reset)
            {
                all_blank_convertToZero(false);
                all_year_show.Text = "";
                tax_show.Text = "";
                total_show.Text = "";
            }
            else
            {
                all_blank_convertToZero(true);
                if (Reduced <= 150000) tax = 0;
                else if (Reduced > 150000 && Reduced <= 300000) tax = (Reduced - 150000) * 0.05;
                else if (Reduced > 300000 && Reduced <= 500000) tax = ((Reduced - 300000) * 0.10) + 7500;
                else if (Reduced > 500000 && Reduced <= 750000) tax = ((Reduced - 500000) * 0.15) + 27500;
                else if (Reduced > 750000 && Reduced <= 1000000) tax = ((Reduced - 750000) * 0.20) + 65000;
                else if (Reduced > 1000000 && Reduced <= 2000000) tax = ((Reduced - 1000000) * 0.25) + 115000;
                else if (Reduced > 2000000 && Reduced <= 5000000) tax = ((Reduced - 2000000) * 0.30) + 365000;
                else if (Reduced > 5000000) tax = ((Reduced - 5000000) * 0.35) + 1265000;
                if (Reduced <= 0) Reduced = 0;
                all_year_show.Text = (Salary_allyear).ToString();
                tax_show.Text = tax.ToString();
                total_show.Text = Reduced.ToString();
            }
        }

        private void TaxGroup1_KeyPress(object sender, KeyEventArgs e)
        {
            all_blank_convertToZero(true);
            Salary_allyear = double.Parse(Salary_month.Text) * 12;
            double child61 = double.Parse(child_after_61.Text);
            double child60 = double.Parse(child_before_61.Text) * 30000;
            double sumhealthcare = double.Parse(HealthcareTextBox.Text);
            double carepeople = double.Parse(Disabled_people.Text) * 60000;
            double momdad = double.Parse(Father_Mother.Text);
            double Temp = Salary_allyear * 0.5;

            if (momdad > 4) momdad = 4 * 30000;
            else momdad = momdad * 30000;

            if (child61 >= 1)
            {
                if (child60 == 0) child61 = (child61 * 60000) - 30000;
                else child61 = child61 * 60000;
            }

            if (sumhealthcare > 60000) sumhealthcare = 60000;

            if ((StatusDropdownbox.Text == "คู่สมรสไม่มีรายได้") && (Temp > 100000))
            {
                sum_group1 = sumhealthcare + carepeople + child60 + child61 + momdad + 120000 + 100000;
                temp_100000 = 100000;
            }
            else if (Temp > 100000)
            {
                sum_group1 = sumhealthcare + carepeople + child60 + child61 + momdad + 60000 + 100000;
                temp_100000 = 100000;
            }
            else
            {
                sum_group1 = sumhealthcare + carepeople + child60 + child61 + momdad + 60000 + Temp;
                temp_100000 = Temp;
            }

            Show_Group1.Text = sum_group1.ToString();
            Delete_Zero();
        }

        private void TaxGroup2_KeyPress(object sender, KeyEventArgs e)
        {
            all_blank_convertToZero(true);
            double donate_spec =  double.Parse(donate_education.Text)
                                + double.Parse(donate_Hospital.Text)
                                + double.Parse(donate_sport.Text)
                                + double.Parse(donate_social.Text);
            donate_spec = donate_spec * 2;

            double cal_for_donate = 0.1 * (Salary_allyear - (sum_group1 + sum_group3 + sum_group4));
            if (donate_spec >= cal_for_donate)
                donate_spec = cal_for_donate;

            double donate = double.Parse(donate_etc.Text);
            if (donate >= Salary_allyear * 0.1)
                donate = Salary_allyear * 0.1;

            double donate_democracy = double.Parse(donate_democracyText.Text);
            if (donate_democracy >= 10000)
                donate_democracy = 10000;

            sum_group2 = donate_spec + donate + donate_democracy;
            if (sum_group2 < 0) sum_group2 = 0;
            Show_Group2.Text = sum_group2.ToString();
            Delete_Zero();
        }

        private void TaxGroup3_KeyPress(object sender, KeyEventArgs e)
        {
            all_blank_convertToZero(true);
            sum_group3 = 0;
            double FifteenPercent = (int.Parse(Salary_month.Text) * 12) * 0.15;
            double temp = 0;

            // ประกันสังคม
            if (double.Parse(Social_insurance.Text) >= 9000) sum_group3 += 9000;
            else sum_group3 += double.Parse(Social_insurance.Text);

            // ประกันชีวิต สุขภาพ
            double life = double.Parse(Life_insurance.Text);
            double health = double.Parse(Health_insurance.Text);

            if (life >= 100000) life = 100000;
            if (health >= 15000) health = 15000;

            if (life + health >= 100000) sum_group3 += 100000;
            else sum_group3 += life + health;

            // ประกันพ่อแม่
            if (double.Parse(Father_Mother_insurance.Text) >= 15000) sum_group3 += 15000;
            else sum_group3 += double.Parse(Father_Mother_insurance.Text);

            //
            if (Husban_Wife_HavemoneyCheckBox.Checked) sum_group3 += 10000;

            // กองทุนการออมแห่งชาติ กอช.
            if (double.Parse(NSF.Text) >= 13200) sum_group3 += 13200;
            else sum_group3 += double.Parse(NSF.Text);

            // กองทุนสำรองเลี้ยงชีพ
            if (double.Parse(Provident_fund.Text) >= 10000)
            {
                sum_group3 += 10000;
                temp += 10000;
            }
            else if (double.Parse(Provident_fund.Text) < 10000)
            {
                sum_group3 += double.Parse(Provident_fund.Text);
                temp += double.Parse(Provident_fund.Text);
            }

            ////
            if ((double.Parse(Provident_fund.Text) > 10000) && (double.Parse(Provident_fund.Text) - 10000 <= FifteenPercent) && (double.Parse(Provident_fund.Text)-10000 <= 490000))
            {
                sum_group3 += double.Parse(Provident_fund.Text) - 10000;
                temp += double.Parse(Provident_fund.Text) - 10000;
            }
            else if ((double.Parse(Provident_fund.Text) - 10000 <= FifteenPercent) && (double.Parse(Provident_fund.Text) - 10000 >= 490000) && (double.Parse(Provident_fund.Text) > 10000))
            {
                sum_group3 += 490000;
                temp += 490000;
            }
            else if ((double.Parse(Provident_fund.Text) - 10000 >= FifteenPercent) && (double.Parse(Provident_fund.Text) - 10000 >= 490000) && (double.Parse(Provident_fund.Text) > 10000))
            {
                sum_group3 += FifteenPercent;
                temp += FifteenPercent;
            }
            else if ((double.Parse(Provident_fund.Text) - 10000 >= FifteenPercent) && (double.Parse(Provident_fund.Text) - 10000 <= 490000) && (double.Parse(Provident_fund.Text) > 10000))
            {
                sum_group3 += FifteenPercent;
                temp += FifteenPercent;
            }

            // กองทุนบำเหน็จบำนาญข้าราชการ กบข
            if (double.Parse(GPF.Text) >= FifteenPercent)
            {
                sum_group3 += FifteenPercent;
                temp += FifteenPercent;
            }
            else if (double.Parse(GPF.Text) >= 500000)
            {
                sum_group3 += 500000;
                temp += 500000;
            }
            else
            {
                sum_group3 += double.Parse(GPF.Text);
                temp += double.Parse(GPF.Text);
            }
            //

            if (int.Parse(LTF_Year.Text) >= 7)
            {
                if ((double.Parse(LTF_Money.Text) <= FifteenPercent) && (double.Parse(LTF_Money.Text) <= 500000))
                    sum_group3 += double.Parse(LTF_Money.Text);
                else if ((double.Parse(LTF_Money.Text) <= FifteenPercent) && (double.Parse(LTF_Money.Text) > 500000))
                    sum_group3 += 500000;
                else if ((double.Parse(LTF_Money.Text) >= FifteenPercent) && (double.Parse(LTF_Money.Text) <= 500000))
                    sum_group3 += FifteenPercent;
                else if ((double.Parse(LTF_Money.Text) > FifteenPercent) && (double.Parse(LTF_Money.Text) > 500000))
                    sum_group3 += 500000;
            }
            //
            if ((double.Parse(RMF_Money.Text) <= FifteenPercent) && (double.Parse(RMF_Money.Text) <= 500000))
            {
                sum_group3 += double.Parse(RMF_Money.Text);
                temp += double.Parse(RMF_Money.Text);
            }
            else if ((double.Parse(RMF_Money.Text) <= FifteenPercent) && (double.Parse(RMF_Money.Text) > 500000))
            {
                sum_group3 += 500000;
                temp += 500000;
            }
            else if ((double.Parse(RMF_Money.Text) > FifteenPercent) && (double.Parse(RMF_Money.Text) > 500000))
            {
                sum_group3 += FifteenPercent;
                temp += FifteenPercent;
            }

            // เบี้ยประกันชีวิตแบบบำนาญ
            if ((double.Parse(PensionTextBox.Text) <= FifteenPercent) && (double.Parse(PensionTextBox.Text) <= 200000))
            {
                if (temp + double.Parse(PensionTextBox.Text) <= 500000) sum_group3 += double.Parse(PensionTextBox.Text);
                else sum_group3 += 500000 - temp;
            }
            else if ((double.Parse(PensionTextBox.Text) <= FifteenPercent) && (double.Parse(PensionTextBox.Text) >= 200000))
            {
                if (temp + double.Parse(PensionTextBox.Text) <= 500000) sum_group3 += 200000;
                else sum_group3 += 500000 - temp;
            }
            else if ((double.Parse(PensionTextBox.Text) >= FifteenPercent) && (double.Parse(PensionTextBox.Text) >= 200000))
            {
                if (temp + double.Parse(PensionTextBox.Text) <= 500000) sum_group3 += 200000;
                else sum_group3 += 500000 - temp;
            }
            else if ((double.Parse(PensionTextBox.Text) >= FifteenPercent) && (double.Parse(PensionTextBox.Text) <= 200000))
            {
                if (temp + double.Parse(PensionTextBox.Text) <= 500000) sum_group3 += FifteenPercent;
                else sum_group3 += 500000 - temp;
            }

            //
            Show_Group3.Text = sum_group3.ToString();
            Delete_Zero();
        }

        private void TaxGroup4_KeyPress(object sender, KeyEventArgs e)
        {
            all_blank_convertToZero(true);
            double Dorgbear_Group4 = double.Parse(DorgBear.Text); //จำกัด 100000
            double FirstHouse = double.Parse(Firsthouse_money.Text);

            if (Dorgbear_Group4 >= 100000) Dorgbear_Group4 = 100000;

            if (Firsthouse_ComboBox.Text == "2562")
            {
                if ((FirstHouse <= 5000000) || (FirstHouse >= 200000)) FirstHouse = 200000;
                else if (FirstHouse >= 5000000) FirstHouse = 0;
            }
            else if ((Firsthouse_ComboBox.Text == "2558") || (Firsthouse_ComboBox.Text == "2559"))
            {
                if (FirstHouse <= 3000000) FirstHouse = FirstHouse * 0.04;
            }

            sum_group4 = FirstHouse + Dorgbear_Group4;
            Show_Group4.Text = sum_group4.ToString();
            Delete_Zero();
        }

        private void Status_swap(object sender, EventArgs e)
        {
            if (StatusDropdownbox.Text == "โสด")
            {
                child_before_61.Enabled = true;
                child_after_61.Enabled = true;
                HealthcareTextBox.Enabled = false;
            }
            else
            {
                child_before_61.Enabled = true;
                child_after_61.Enabled = true;
                HealthcareTextBox.Enabled = true;
            }
        }

        private void num_check(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
        }

        private void Buyfirsthouse_check(object sender, EventArgs e)
        {
            if(Firsthouse_ComboBox.Enabled == false) Firsthouse_ComboBox.Enabled = true;
            else if (Firsthouse_ComboBox.Enabled == true) Firsthouse_ComboBox.Enabled = false;
            if (Firsthouse_money.Enabled == false) Firsthouse_money.Enabled = true;
            else if (Firsthouse_money.Enabled == true) Firsthouse_money.Enabled = false;
        }
        private void all_blank_convertToZero(bool swap)
        {
            Regex regex = new Regex("^[0-9]");
            List<TextBox> textboxcontainer = new List<TextBox>
            {
                Salary_month, child_before_61, child_after_61, HealthcareTextBox, Disabled_people,Father_Mother , //6
                donate_education, donate_Hospital, donate_sport, donate_social, donate_etc, donate_democracyText, //6
                Social_insurance, Life_insurance, Health_insurance, Father_Mother_insurance, Provident_fund, GPF, //6
                NSF, PensionTextBox, LTF_Money, RMF_Money, LTF_Year, DorgBear, Firsthouse_money //7
            };
            if (swap)
            {
                foreach (TextBox a in textboxcontainer)
                {
                    if ((a.Text == "") || (a.Text.Trim().StartsWith("-")) || (!regex.IsMatch(a.Text))) a.Text = "0";
                    a.SelectionStart = a.Text.Length;
                    a.SelectionLength = 0;
                }
            }
            else
            {
                foreach (TextBox a in textboxcontainer)
                {
                    a.Text = "0";
                }
                Show_Group1.Text = "0";
                Show_Group2.Text = "0";
                Show_Group3.Text = "0";
                Show_Group4.Text = "0";
                if ((Father_Mother.Text == "") || (Father_Mother.Text != "0")) Father_Mother.Text = "0";
                if ((Firsthouse_ComboBox.Text == "") || (Firsthouse_ComboBox.Text != "0")) Firsthouse_ComboBox.Text = "0";
                if ((StatusDropdownbox.Text == "") || (StatusDropdownbox.Text != "โสด")) StatusDropdownbox.Text = "โสด";
            }
            if ((Father_Mother.Text == "")) Father_Mother.Text = "0";
            if ((Firsthouse_ComboBox.Text == "")) Firsthouse_ComboBox.Text = "0";
            if ((StatusDropdownbox.Text == "")) StatusDropdownbox.Text = "โสด";
        }
        private void Delete_Zero()
        {
            List<TextBox> textboxcontainer = new List<TextBox>
            {
                Salary_month, child_before_61, child_after_61, HealthcareTextBox, Disabled_people,Father_Mother , //6
                donate_education, donate_Hospital, donate_sport, donate_social, donate_etc, donate_democracyText, //6
                Social_insurance, Life_insurance, Health_insurance, Father_Mother_insurance, Provident_fund, GPF, //6
                NSF, PensionTextBox, LTF_Money, RMF_Money, LTF_Year, DorgBear, Firsthouse_money //7
            };
            foreach (TextBox a in textboxcontainer)
            {
                if (a.Text.StartsWith("0"))
                {
                    a.Text = a.Text.Remove(0,1);
                }
            }
        }
    }
}
