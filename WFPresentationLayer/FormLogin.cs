using BLL;
using BusinessLogicalLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPresentationLayer
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            new ClienteService().Insert(new Cliente()
            {
                Nome = "Danizinho Bernart",
                EhAtivo = true,
                CPF = "901.917.069-49",
                DataNascimento = DateTime.Now.AddYears(-25),
                Email = "matfts2fsd@gmail.com"
            });

        }

        //private FuncionarioBLL funcionarioBLL = new FuncionarioBLL();

        private void button1_Click(object sender, EventArgs e)
        {
            new ClienteService().Insert(new Cliente()
            {
                Nome = "Danizinho Bernart",
                EhAtivo = true,
                CPF = "901.917.069-49",
                DataNascimento = DateTime.Now.AddYears(-25),
                Email = "matfts2fsd@gmail.com"
            });

            new ClienteService().Insert(new Cliente());

            //DataResponse<Funcionario> response = funcionarioBLL.Autenticar(txtEmail.Text, txtSenha.Text);
            //if (response.Sucesso)
            //{
            //    FormMenu frmMenu = new FormMenu();
            //    this.Hide();
            //    frmMenu.ShowDialog();
            //    //Esta linha só executa quando o FormMenu for fechado
            //    this.Show();
            //}
            //else
            //{
            //    MessageBox.Show(response.GetErrorMessage());
            //}
        }
    }
}
