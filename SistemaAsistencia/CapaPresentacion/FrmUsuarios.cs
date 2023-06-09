﻿using SistemaAsistencia.CapaModelo;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaAsistencia.CapaVistas
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();



        }
        CapaModelo.ClsImage imageUser = new CapaModelo.ClsImage();

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            CapaDatos.ClsUsuarioBD data = new CapaDatos.ClsUsuarioBD();
            dt = data.LlenarUsuarios_db();
            dataUsuario.DataSource = dt;

            //this.pictureUser.Image = Image.FromFile(@"C:\Users\PC01AQ\Desktop\SistemaAsistencia\SistemaAsistencia\Assets\gifUser.gif");
            //this.pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CapaVistas.FrmAgregarUsuario agregarUsuario = new CapaVistas.FrmAgregarUsuario();
            agregarUsuario.ShowDialog();

        }
        private void btnModificar_Click(object sender, EventArgs e)
        {

            CapaVistas.FrmModificarUsuario modificarUser = new CapaVistas.FrmModificarUsuario();

            if (dataUsuario.Rows.Count != 0)
            {

                modificarUser.labelId.Text = dataUsuario.CurrentRow.Cells[0].Value.ToString();
                modificarUser.txtCedula.Text = dataUsuario.CurrentRow.Cells[1].Value.ToString();
                modificarUser.txtName.Text = dataUsuario.CurrentRow.Cells[2].Value.ToString();
                modificarUser.txtLastName.Text = dataUsuario.CurrentRow.Cells[3].Value.ToString();
                modificarUser.txtCorreo.Text = dataUsuario.CurrentRow.Cells[4].Value.ToString();
                modificarUser.txtTelefono.Text = dataUsuario.CurrentRow.Cells[5].Value.ToString();
                modificarUser.cedulaValidada = dataUsuario.CurrentRow.Cells[6].Value.ToString();
                modificarUser.combotxtRol.Text = dataUsuario.CurrentRow.Cells[7].Value.ToString();
                modificarUser.comboEstado.Text = dataUsuario.CurrentRow.Cells[8].Value.ToString();
                modificarUser.txtFile.Text = dataUsuario.CurrentRow.Cells[9].Value.ToString();
                byte[] photoByte = (byte[])dataUsuario.CurrentRow.Cells[10].Value;

                Image imagenUserPhoto = imageUser.byteArrayToImage(photoByte);
                modificarUser.pictureUser.Image = imagenUserPhoto;
                modificarUser.arrayImagenUser = photoByte;
                modificarUser.Show();
            }
            else
            {
                MessageBox.Show("Elija el dato que va a modificar", "Notificación");
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

        private void panel1_Resize(object sender, EventArgs e)
        {
            panelUsuario.Left = (Width - panelUsuario.Width) / 2;
            panelUsuario.Top = (Height - panelUsuario.Height) / 2;
        }
    }
}
