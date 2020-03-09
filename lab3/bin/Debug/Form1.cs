using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }

        //on save 
        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            
            if(IsValidate())
            {
                try
                {
                    this.productsBindingSource.EndEdit();
                    this.order_DetailsBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.nORTHWNDDataSet);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Argument Exception");
                    productsBindingSource.CancelEdit();
                }
                catch (DBConcurrencyException) // cataching data adapter error while inserting or dupdating error
                {
                    MessageBox.Show("A concurrency error occurred. " +
                        "Some rows were not updated. ", "Save Error");
                    this.productsTableAdapter.Fill(this.nORTHWNDDataSet.Products);
                }
                catch (DataException ex) //catching errors using ado.net
                {
                    
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                    productsBindingSource.CancelEdit();
                }
                catch (SqlException ex) //server error
                {
                    
                    MessageBox.Show(
                        "Database error # " + ex.Number +
                        ": " + ex.Message, ex.GetType().ToString());
                }
              
            }else
            {
                try
                {
                   
                    this.tableAdapterManager.UpdateAll(this.nORTHWNDDataSet);
                }
                catch (DBConcurrencyException)
                {
                    MessageBox.Show("A concurrency error occurred. " +
                        "Some rows were not updated. ");
                    this.productsTableAdapter.Fill(this.nORTHWNDDataSet.Products);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(
                        "Database error # " + ex.Number +
                        ": " + ex.Message, ex.GetType().ToString());
                }
            }
            
          
        }

        //validating inputs
        private bool IsValidate()
        {
            return
                IsInt(txtProductId, "ProductID") &&
                IsInt(txtUnitsInStock, "UnitInStock") &&
                IsInt(txtUnitsOnOrder, "UnitsOnOrder") &&
                IsInt(txtReOrderLevel, "ReorderLevel") &&
                IsShort(txtUnitPrice, "UnitPrice");


        }

        //making sure the input is an integer
        public bool IsInt (TextBox tb, string name)
        {
            bool isOkay = true  ;
           int val = 0;
           if (int.TryParse(tb.Text, out val))
            {
                isOkay = true;
            }
            else
            {
                isOkay = false;
                MessageBox.Show(name + "has to be a positive whole number!");
            }
            return isOkay;
              
        }

        //making sure the input is float
        public bool IsShort (TextBox tb, string name)
        {
            bool isOkay = true;
            float val = 0;
            if (float.TryParse (tb.Text, out val))
            {
                isOkay = true;
            }
            else
            {
                isOkay = false;
                MessageBox.Show(name + "has to be positve number!");
            }
            return isOkay;
        }

   
        //on form load
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nORTHWNDDataSet.Order_Details' table. You can move, or remove it, as needed.
            this.order_DetailsTableAdapter.Fill(this.nORTHWNDDataSet.Order_Details);
            // TODO: This line of code loads data into the 'nORTHWNDDataSet.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.nORTHWNDDataSet.Categories);
            // TODO: This line of code loads data into the 'nORTHWNDDataSet.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.nORTHWNDDataSet.Suppliers);

            try
            {
                
                // TODO: This line of code loads data into the 'nORTHWNDDataSet.Products' table. You can move, or remove it, as needed.
                this.productsTableAdapter.Fill(this.nORTHWNDDataSet.Products);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
       

        }

        //following block for filling in order table
        private void txtProductId_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                int productID = Convert.ToInt32(txtProductId.Text);
                this.order_DetailsTableAdapter.FillByProductID
                    (this.nORTHWNDDataSet.Order_Details,productID);
            }catch(Exception ex)
            {
                MessageBox.Show( ex.Message, ex.GetType().ToString());
            }
        }

    

    }
}
