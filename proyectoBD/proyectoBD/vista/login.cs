﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoBD
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            password.isPassword = true;
        }

        bool f = true;
        public int index;
        public string UsernameID;
        public string NameUser;
        public string LastNameUser;

        private void login_Load(object sender, EventArgs e)
        {

            try {
                Console.WriteLine("antes de cargar");
                //Console.WriteLine(s.Cargar().getDatosUser().getUsers()[0].UserName);
                Console.WriteLine("despues de cargar");

                //Console.WriteLine("-- " + du.getUsers()[0].UserName + "  " + du.getUsers()[0].Password);

            } catch(NullReferenceException nre){
                new popup(nre.Message, popup.AlertType.error);
            }
            

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            loginDiag d = new loginDiag("¿Desea salir?", true, false, true);
            d.Show();
        }

        private void password_Enter(object sender, EventArgs e)
        {
            if (f)
            {
                password.Text = "";
                f = false;
            }
        }

        private void username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                password.Focus();
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ingresar_Click(null, null);

            }
        }

        private void ingresar_Click(object sender, EventArgs e)
        {

            if (verifica())
            {
                Console.WriteLine("entra");
                new viewCarga(viewCarga.type.inicio);

                this.Hide();
                new popup("Bienvenido...", popup.AlertType.check);
            }
            else
            {
                //loginDiag d = new loginDiag("Error, usuario o contraseña incorrectos", false, true, false);
                //d.Show();
                new popup("Usuario o contraseña incorrectos", popup.AlertType.error);
            }
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            Register r = new Register(false,"");
            r.Show();
        }

        private bool verifica()
        {
            user v = new user();
            bool ver = false;
            try
            {   
                DataTable Datos = v.Verifica(this.username.Text,this.password.Text);
                if (Datos.Rows.Count != 0)
                {ver= true;}
                else {ver= false;}
                DataRow row = Datos.Rows[0];
                UsernameID = row["id"].ToString();
                NameUser = row["nombre"].ToString();
                LastNameUser = row["apellido"].ToString();
                return ver;
            }
            catch (Exception ex)
            {
                new popup(ex.Message, popup.AlertType.error);
                return ver;
            }
        }

        private void password_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
