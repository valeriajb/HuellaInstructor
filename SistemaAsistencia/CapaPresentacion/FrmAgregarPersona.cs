﻿using SistemaAsistencia.CapaDatos;
using SistemaAsistencia.CapaModelo;
using SistemaAsistencia.CapaNegocio;
using SistemaAsistencia.CapaPresentacion;
using System;
using System.Data;
using System.Windows.Forms;
namespace SistemaAsistencia.CapaVistas
{
    public partial class FrmAgregarPersona : Form
    {
        public FrmAgregarPersona()
        {
            InitializeComponent();
            Data = new AppData();
        }
        Byte[] arrayImagenUser;
        public AppData Data;
        public int i;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            ClsOpenDialogPicture abrirCadena = new ClsOpenDialogPicture(this.txtFile.Text, this.pictureUser.Image);
            try
            {
                this.txtFile.Text = abrirCadena.AbrirCadena(this.txtFile.Text);
                this.pictureUser.Image = abrirCadena.MostrarImagen(this.txtFile.Text);

                ClsImage imagenArray = new ClsImage();
                arrayImagenUser = imagenArray.ImageToByteArray(this.pictureUser.Image);
            }
            catch
            {
                MessageBox.Show("Escoja una foto", "Notificación");
            }
        }


        public static void SeleccionarDedo()
        {
            //if ((string.IsNullOrEmpty(this.txtNoDedo.Text))
            //{
            //    this.txt
            //}

        }




        private void btnAgregar_Click(object sender, EventArgs e)
        {

            int cantidadHuellas;
            if (this.txtNoDedo.Text == "")
            {
                MessageBox.Show("Ingrese el código de barras", "Notificación");
            }
            else
            {
                if (this.txtName.Text == "")
                {
                    MessageBox.Show("Ingrese el nombre de la Persona", "Notificación");
                }
                else
                {
                    if (this.txtLastName.Text == "")
                    {
                        MessageBox.Show("Ingrese el apellido de la persona", "Notificación");
                    }
                    else
                    {
                        if (this.txtCorreo.Text == "" || ClsValidarCorreo.validarEmail(this.txtCorreo.Text) == false)
                        {
                            MessageBox.Show("Ingrese un correo válido", "Notificación");
                        }
                        else
                        {
                            if (this.txtTelefono.Text == "")
                            {
                                MessageBox.Show("Ingrese el teléfono de la persona", "Notificación");

                            }
                            else
                            {
                                if (this.comboFuncionario.Text == "")
                                {
                                    MessageBox.Show("Ingrese el rol del funcionario", "Notificación");
                                }
                                else
                                {
                                    if (this.txtFicha.Text == "")
                                    {
                                        MessageBox.Show("Ingrese la ficha", "Notificación");
                                    }
                                    else
                                    {
                                        if (this.txtFile.Text == "")
                                        {
                                            MessageBox.Show("Ingrese una imagen", "Notificación");
                                        }
                                        else
                                        {
                                            cantidadHuellas = 0;

                                            for (int i = 0; i < 10; i++)
                                            {
                                                if (Data.Templates[i] != null)
                                                {
                                                    cantidadHuellas++;

                                                }
                                            }
                                            if ((cantidadHuellas == 0) && (this.enrollmentControl1.EnrolledFingerMask == 0))
                                            {
                                                MessageBox.Show("No se puede guardar el registro, debe registrar mínimo una huella dáctilar", "Error");
                                                this.txtCedula.Focus();
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    cantidadHuellas = 0;
                                                    for (i = 0; i < 10; i++)
                                                    {
                                                        if ((Data.Templates[i] != null))
                                                        {
                                                            cantidadHuellas++;
                                                            this.txtNoDedo.Text = (i + 1).ToString();

                                                            //ClsPersona clsPersona1 = new ClsPersona(arrayImagenUser, this.txtName.Text, this.txtLastName.Text, this.txtCorreo.Text, this.txtTelefono.Text, this.comboFuncionamiento.Text, this.txtFicha.Text, this.txtFile.Text, arrayImagenUser, int.Parse(this.txtCodigo.Text));
                                                            ClsPersonaBD clsPersona = new ClsPersonaBD();
                                                            DataTable data = new DataTable();
                                                            data = clsPersona.Validar_Cedula(this.txtCedula.Text);

                                                            if (data.Rows.Count == 0)
                                                            {
                                                                //{
                                                                //    DataTable dataTable = new DataTable();
                                                                //    dataTable = clsPersona.Validar_Codigo(int.Parse(this.txtNoDedo.Text));

                                                                //if (dataTable.Rows.Count == 0)
                                                                //{

                                                                clsPersona.AgregarPersona_db(Data.Templates[i].Bytes, this.txtCedula.Text, this.txtName.Text, this.txtLastName.Text, this.txtCorreo.Text, this.txtTelefono.Text, this.comboFuncionario.Text, this.txtFicha.Text, this.txtFile.Text, arrayImagenUser, int.Parse(this.txtNoDedo.Text));


                                                                FrmPersonas frmPersonas = new FrmPersonas();
                                                                DataTable dataPeople = new DataTable();
                                                                dataPeople = clsPersona.LlenarDatos();
                                                                frmPersonas.dataPersona.DataSource = dataPeople;

                                                                //Mensaje de Salida para que el usuario sepa que está agregado el Usuario 
                                                                MessageBox.Show("Persona Agregada", "Notiicación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                this.txtNoDedo.Text = "";
                                                                this.txtName.Text = "";
                                                                this.txtLastName.Text = "";
                                                                this.txtCorreo.Text = "";
                                                                this.txtTelefono.Text = "";
                                                                this.comboFuncionario.Text = "";
                                                                this.txtFile.Text = "";
                                                                this.txtFicha.Text = "";
                                                                this.pictureUser.Image = null;
                                                            }
                                                            //}
                                                            //else
                                                            //{
                                                            //    MessageBox.Show("Este código ya existe", "Notificación");
                                                            //}
                                                            else
                                                            {
                                                                MessageBox.Show("Ya existe una persona con esta cédula");
                                                                int idPersona = int.Parse(data.Rows[0][0].ToString());

                                                            }
                                                        }
                                                       
                                                    }
                                                
                                                }
                                                catch (Exception e1)
                                            {
                                                MessageBox.Show("No se puede guardar el registro en la base de datos" + e1.ToString(), "Error");
                                                this.txtNoDedo.Focus();

                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

    } 


        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar el proceso?", "Notificación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.txtNoDedo.Text = "";
                this.txtName.Text = "";
                this.txtLastName.Text = "";
                this.txtCorreo.Text = "";
                this.txtTelefono.Text = "";
                this.comboFuncionario.Text = "";
                this.txtFile.Text = "";
                this.txtFicha.Text = "";
                this.pictureUser.Image = null;
                this.Hide();

            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    if (char.IsPunctuation(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }


            }
        }

        private void txtFicha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    if (char.IsPunctuation(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }


            }
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void enrollmentControl1_OnDelete(object Control, int FingerMask, ref DPFP.Gui.EventHandlerStatus EventHandlerStatus)
        {
            if (Data.IsEventHandlerSucceeds)
            {
                Data.Templates[FingerMask - 1] = null;
            }
            else
            {
                EventHandlerStatus = DPFP.Gui.EventHandlerStatus.Failure;
            }
        }

        private static void LimpiarHuella(FrmAgregarPersona x)
        {
            for (int i = 0; i < 10; i++)
            {
                x.Data.Templates[i] = null;
            }
            x.txtNoDedo.Text = "0";
        }

        private void enrollmentControl1_OnEnroll(object Control, int FingerMask, DPFP.Template Template, ref DPFP.Gui.EventHandlerStatus EventHandlerStatus)
        {
            if (Data.IsEventHandlerSucceeds)
            {
                LimpiarHuella(this);
                Data.Templates[FingerMask - 1] = Template;
            }
            else
            {
                EventHandlerStatus = DPFP.Gui.EventHandlerStatus.Failure;
            }
        }

        private void enrollmentControl1_OnStartEnroll(object Control, string ReaderSerialNumber, int Finger)
        {
            this.txtNoDedo.Text = Finger.ToString();
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    if (char.IsPunctuation(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }


            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    if (char.IsPunctuation(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }


            }
        }

        private void txtFicha_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    if (char.IsPunctuation(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }


            }
        }

        private void comboFuncionario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtFile_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtName_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            {
                if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                {
                    e.Handled = true;
                    return;

                }
            }
        }
    }
}
