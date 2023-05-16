using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRUD_ADO.NET
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            string query2 = "select * from Employee where ID= @ID";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.Parameters.AddWithValue("@ID", idTB.Text);
            con.Open();
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.HasRows == true)
            {
                MessageBox.Show(
                    idTB.Text + "Id already exists!",
                    "Failure",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                con.Close();
            }
            else
            {
                con.Close();

                //values fetch by parameters insert into database column
                string query1 =
                    "insert into Employee values(@ID, @NAME, @GENDER, @AGE, @DESIG, @SALARY)";

                SqlCommand cmd1 = new SqlCommand(query1, con);

                // Retrieve data from user input and assign into parameters
                cmd1.Parameters.AddWithValue("@ID", idTB.Text);
                cmd1.Parameters.AddWithValue("@NAME", nameTB.Text);
                cmd1.Parameters.AddWithValue("@GENDER", genderCB.SelectedItem);
                cmd1.Parameters.AddWithValue("@AGE", ageNUD.Value);
                cmd1.Parameters.AddWithValue("@DESIG", dCB.SelectedItem);
                cmd1.Parameters.AddWithValue("@SALARY", salaryTB.Text);

                con.Open();
                int noOfRows = cmd1.ExecuteNonQuery();
                if (noOfRows > 0)
                {
                    MessageBox.Show(
                        "Inserted Successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    BindGridView();
                }
                else
                {
                    MessageBox.Show(
                        "Insertion Failed!",
                        "Failure",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                con.Close();
                ResetControls();
            }
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Employee";

            // Fetch data from the database
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            // Single table
            DataTable dt = new DataTable();

            // Fill the data into table retrieved from database using datareader
            sda.Fill(dt);

            // pass the datatable object dt to datagridview and stored the data into datagridview using datasource property
            dataGridView1.DataSource = dt;
        }

        private void viewBtn_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            //values fetch by parameters modify to database column
            string query =
                "update Employee set ID=@ID, NAME= @NAME, GENDER= @GENDER, AGE= @AGE, DESIGNATION= @DESIG, SALARY= @SALARY where ID= @ID";
            SqlCommand cmd = new SqlCommand(query, con);

            // Retrieve data from user input and assign into parameters
            cmd.Parameters.AddWithValue("@ID", idTB.Text);
            cmd.Parameters.AddWithValue("@NAME", nameTB.Text);
            cmd.Parameters.AddWithValue("@GENDER", genderCB.SelectedItem);
            cmd.Parameters.AddWithValue("@AGE", ageNUD.Value);
            cmd.Parameters.AddWithValue("@DESIG", dCB.SelectedItem);
            cmd.Parameters.AddWithValue("@SALARY", salaryTB.Text);

            con.Open();
            int noOfRows = cmd.ExecuteNonQuery();
            if (noOfRows > 0)
            {
                MessageBox.Show(
                    "Updated Successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                BindGridView();
            }
            else
            {
                MessageBox.Show(
                    "Updation Failed!",
                    "Failure",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            con.Close();
            ResetControls();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            idTB.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTB.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            genderCB.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            ageNUD.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value);
            dCB.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            salaryTB.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            //values fetch by parameters modify to database column
            string query = "delete from Employee where ID= @ID";
            SqlCommand cmd = new SqlCommand(query, con);

            // Retrieve data from user input and assign into parameters
            cmd.Parameters.AddWithValue("@ID", idTB.Text);

            con.Open();
            int noOfRows = cmd.ExecuteNonQuery();
            if (noOfRows > 0)
            {
                MessageBox.Show(
                    "Deleted Successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                BindGridView();
            }
            else
            {
                MessageBox.Show(
                    "Deletion Failed!",
                    "Failure",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            con.Close();
            ResetControls();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        void ResetControls()
        {
            // Reset values to default
            idTB.Clear();
            nameTB.Clear();
            genderCB.SelectedItem = null;
            ageNUD.Value = 20;
            dCB.SelectedItem = null;
            salaryTB.Clear();
            idTB.Focus(); // Cursor goes to Id field
        }
    }
}
