using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eff
{
    public partial class Form1 : Form
    {
        CompanyEntities context = new CompanyEntities();
        int Dno;
        public Form1()
        {
            InitializeComponent();

            cmbDept.DisplayMember = "Dname";
            cmbDept.ValueMember = "Dnum";
            cmbDept.DataSource = context.Departments.Select(d => d).ToList();
        }

        private void RefreshData()
        {
            lbxData.DisplayMember = "Fname";

            Dno = (int)cmbDept.SelectedValue;
            lbxData.DataSource = context.Employees.Where(em => em.Dno == Dno).ToList();
        }

        private void RefreshGrid()
        {
            Dno = (int)cmbDept.SelectedValue;
            grdData.DataSource = context.Employees.Where(em => em.Dno == Dno).ToList();
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();  
            RefreshGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            var employee = new Employee()
            {
                SSN = Int32.Parse(txtSSN.Text),
                Fname = txtFname.Text,
                Lname = txtLname.Text,
                Salary = Int32.Parse(txtSalary.Text),
                Dno = (int)cmbDept.SelectedValue
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            RefreshGrid();
            RefreshData();

            MessageBox.Show("Added Success");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            Employee empOLd = (Employee)lbxData.SelectedItem;
            
            int SSN = empOLd.SSN;

            Employee employee = context.Employees.Where(em => em.SSN == SSN).First();

            employee.SSN = Int32.Parse(txtSSN.Text);
            employee.Fname = txtFname.Text;
            employee.Lname = txtLname.Text;
            employee.Salary = Int32.Parse(txtSalary.Text);
            employee.Dno = (int)cmbDept.SelectedValue;
                
            context.SaveChanges();
            RefreshData();
            RefreshGrid();
            MessageBox.Show("Updated Success");

        }

        private void lbxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee emp = (Employee)lbxData.SelectedItem;

            txtSSN.Text = emp.SSN.ToString();
            txtFname.Text = emp.Fname;
            txtLname.Text = emp.Lname;
            txtSalary.Text = emp.Salary.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int SSN = Int32.Parse(txtSSN.Text);
            var emp = context.Employees.Where(em => em.SSN == SSN).FirstOrDefault();
            context.Employees.Remove(emp);
            context.SaveChanges();

            RefreshData();
            RefreshGrid();
            MessageBox.Show("deleted Success");
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void grdData_SelectionChanged(object sender, EventArgs e)
        {
            Employee emp = (Employee)grdData.CurrentRow.DataBoundItem;

            txtSSN.Text = emp.SSN.ToString();
            txtFname.Text = emp.Fname;
            txtLname.Text = emp.Lname;
            txtSalary.Text = emp.Salary.ToString();
        }
    }
}
