using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySanPham.Models;
namespace QuanLySanPham
{
    public partial class Form1 : Form
    {
        Model1 SP = new Model1();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product NewProduct = new Product();
            NewProduct.ProductID = textBox1.Text;
            NewProduct.ProductName = textBox2.Text;
            NewProduct.CategoryID = (comboBox1.SelectedItem as Category).CategoryID;
            NewProduct.Price = int.Parse(textBox3.Text);
            SP.Products.AddOrUpdate(NewProduct);
            SP.SaveChanges();
            LoadList();
            LoadForm();
        }
        private void LoadList()
        {
            List<Product> NewProductList = SP.Products.ToList();
            BindGrid(NewProductList);
        }
        private void LoadForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Model1 context = new Model1();
            List<Category> CategoryList = context.Categories.ToList();
            List<Product> ProductList = context.Products.ToList();
            comboBox1.DataSource = context.Categories.ToList();
            comboBox1.ValueMember = "CategoryID";
            comboBox1.DisplayMember = "CategoryName";
            BindGrid(ProductList);
        }
        private void BindGrid(List<Product> ProductList)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in ProductList)
            {
                int pos = dataGridView1.Rows.Add();
                dataGridView1.Rows[pos].Cells[0].Value = item.ProductID;
                dataGridView1.Rows[pos].Cells[1].Value = item.ProductName;
                dataGridView1.Rows[pos].Cells[2].Value = item.Category.CategoryName;
                dataGridView1.Rows[pos].Cells[3].Value = item.Price;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }
    }
}
