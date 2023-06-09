﻿using SistemaAsistencia.CapaModelo;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaAsistencia.CapaVistas
{
    public partial class FrmPersonas : Form
    {
        public FrmPersonas()
        {
            InitializeComponent();
        }

        private void FrmPersonas_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            CapaDatos.ClsPersonaBD clsPersona = new CapaDatos.ClsPersonaBD();
            dt = clsPersona.LlenarDatos();
            dataPersona.DataSource = dt;

            dataPersona.Columns[0].Visible = false;
            dataPersona.Columns[1].Visible = false;
            dataPersona.Columns[5].Visible = false;
            dataPersona.Columns[6].Visible = false;
            dataPersona.Columns[8].Visible = false;
            dataPersona.Columns[9].Visible = false;
            dataPersona.Columns[10].Visible = false;


            dataPersona.Columns[2].HeaderText ="Cédula" ;
            dataPersona.Columns[3].HeaderText = "Nombre";
            dataPersona.Columns[4].HeaderText = "Apellido";
            dataPersona.Columns[7].HeaderText = "Funcionario";
            dataPersona.Columns[11].HeaderText = "Código de Barras";


            //this.picturePeople.Image = Image.FromFile(@"C:\Users\PC01AQ\Desktop\SistemaAsistencia\SistemaAsistencia\Assets\gifPeople.gif");
            //this.picturePeople.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CapaVistas.FrmAgregarPersona agregarPersona = new CapaVistas.FrmAgregarPersona();
            agregarPersona.Show();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ClsImage image = new ClsImage();

            CapaVistas.FrmModificarPersona modificarPersona = new CapaVistas.FrmModificarPersona();

            if (dataPersona.Rows.Count != 0)
            {
                modificarPersona.labelId.Text = dataPersona.CurrentRow.Cells[0].Value.ToString();
                modificarPersona.txtCedula.Text = dataPersona.CurrentRow.Cells[2].Value.ToString();
                modificarPersona.txtName.Text = dataPersona.CurrentRow.Cells[3].Value.ToString();
                modificarPersona.txtLastName.Text = dataPersona.CurrentRow.Cells[4].Value.ToString();
                modificarPersona.txtCorreo.Text = dataPersona.CurrentRow.Cells[5].Value.ToString();
                modificarPersona.txtTelefono.Text = dataPersona.CurrentRow.Cells[6].Value.ToString();
                modificarPersona.comboFuncionario.Text = dataPersona.CurrentRow.Cells[7].Value.ToString();
                modificarPersona.txtFicha.Text = dataPersona.CurrentRow.Cells[8].Value.ToString();


                modificarPersona.txtFile.Text = dataPersona.CurrentRow.Cells[9].Value.ToString();
                byte[] photoPerson = (byte[])dataPersona.CurrentRow.Cells[10].Value;
                modificarPersona.txtNoDedo.Text = dataPersona.CurrentRow.Cells[11].Value.ToString();


                Image imagePerson = image.byteArrayToImage(photoPerson);
                modificarPersona.pictureUser.Image = imagePerson;
                modificarPersona.arrayImagenUser = photoPerson;
                modificarPersona.Show();
            }
            else
            {
                MessageBox.Show("Elija el dato que va a modificar","Notificación");
            }
                


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

         

            this.Close();
            FrmMenu menu = new FrmMenu();
            menu.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmMenu menu = new FrmMenu();
            menu.Show();
        }

        private void panelPersona_Resize(object sender, EventArgs e)
        {
            panelPersona.Left = (Width - panelPersona.Width) / 2;
            panelPersona.Top = (Height - panelPersona.Height) / 2;
        }
    }
}
