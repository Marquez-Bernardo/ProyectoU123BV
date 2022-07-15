using Microsoft.EntityFrameworkCore;
using ProyectoU123BV.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProyectoU123BV.Modelo;

namespace ProyectoU123BV
{
    /// <summary>
    /// Lógica de interacción para Sistema.xaml
    /// </summary>
    public partial class Sistema : Window
    {
        public Sistema()
        {
            InitializeComponent();
            GetUsers();
        }

        private void GetUsers()
        {
            //ProductDG.ItemsSource = context.Products.ToList();

            using (var _context = new AplicationdbContext())
            {
                UserTable.ItemsSource = _context.Usuarios.ToList();

            }


        }

        private void SelectItem(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario = (sender as FrameworkElement).DataContext as Usuario;
                txtId.Text = usuario.Id.ToString();
                txtNombre.Text = usuario.Nombre.ToString();
                txtUser.Text = usuario.User.ToString();
                txtPassword.Text = usuario.Password.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception ("Error interno: "+ ex.Message);
            }
        }

        private void Button_Editar(object sender, RoutedEventArgs e)
        {
            Usuario usuarioUpdate = new Usuario();
            usuarioUpdate.Id = int.Parse(txtId.Text);
            using (var _context = new AplicationdbContext())
            {
                usuarioUpdate = _context.Usuarios.Find(usuarioUpdate.Id);
                usuarioUpdate.Nombre = txtNombre.Text;
                usuarioUpdate.User = txtUser.Text;
                usuarioUpdate.Password = txtPassword.Text;
                _context.Entry(usuarioUpdate).State = EntityState.Modified;
                _context.SaveChanges();
                GetUsers();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Usuario eliminar = new Usuario();
            eliminar = (sender as FrameworkElement).DataContext as Usuario;
            using (var _context = new AplicationdbContext())
            {
                eliminar = _context.Usuarios.Find(eliminar.Id);
                _context.Entry(eliminar).State = EntityState.Deleted;
                MessageBox.Show("Eliminacion de usuario exitosa");
                _context.SaveChanges();
                GetUsers();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == null && txtUser.Text == null && txtPassword.Text == null)
            {
                MessageBox.Show("Campos obligatorios vacios");
            }else
            {
                Usuario crear = new Usuario();
                using (var _context = new AplicationdbContext())
                {
                    crear.Nombre = txtNombre.Text;
                    crear.User = txtUser.Text;
                    crear.Password = txtPassword.Text;
                    _context.Add(crear);
                    _context.SaveChanges();
                    GetUsers();
                    txtNombre.Clear();
                    txtUser.Clear();
                    txtPassword.Clear();
                }
            }
        }
    }
}
