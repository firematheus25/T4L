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
using System.Windows.Navigation;
using System.Windows.Shapes;
using T4L.Domain.Entities;
using T4L.Domain.Entities.Vw;
using T4L.WPF.Services;

namespace T4L.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Produto Produto = new Produto();
        public List<ProdutoGrupo> grupos { get; set; }
        public List<VwConsulta> VwConsulta { get; set; }
        public MainWindow()
        {
            BuildComboBox();
            InitializeComponent();
            HiddenButtons();            
        }



        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var produto = BuildProduto();
            if (produto != null)
            {
                var genericResult = await new HttpResquestMethods<Produto>().Post(produto);
                MessageBox.Show(genericResult.Message);
                ClearCadastro();
            }
            
        }

        private async void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            var produto = BuildProduto();
            if (produto != null)
            {
                var genericResult = await new HttpResquestMethods<Produto>().Put(produto);
                MessageBox.Show(genericResult.Message);
                ClearCadastro();
            }
            
        }


        private async void BtnDeletar_Click(object sender, RoutedEventArgs e)
        {
            var genericResult = await new HttpResquestMethods<Produto>().Delete(Produto.Cod);
            MessageBox.Show(genericResult.Message);
            ClearCadastro();
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            ClearCadastro();
        }

        private void btConsulta_Click(object sender, RoutedEventArgs e)
        {
            cvCadastro.Visibility = Visibility.Collapsed;
            cvConsulta.Visibility = Visibility.Visible;
        }

        private void btCadastro_Click(object sender, RoutedEventArgs e)
        {
            cvCadastro.Visibility = Visibility.Visible;
            cvConsulta.Visibility = Visibility.Collapsed;
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbBusca.Text))
            {
                var response = await new HttpResquestMethods<VwConsulta>().GetByKeyWordAsync(txbBusca.Text);
                VwConsulta = (List<VwConsulta>)response.Data;
                DataGridViewProdutos.ItemsSource = VwConsulta;
            }
            else
            {
                var response = await new HttpResquestMethods<VwConsulta>().GetAsync();
                VwConsulta = (List<VwConsulta>)response.Data;
                DataGridViewProdutos.ItemsSource = VwConsulta;
            }
            
        }


        private Produto BuildProduto()
        {
            var result = IsCadastroValid();
            if (result == false) return null;

            Produto.Descricao = txbDescricao.Text.ToUpper();
            Produto.CodGrupo = Convert.ToInt32(cbbGrupoProduto.SelectedValue);
            Produto.CodBarra = txbCodigoBarra.Text;
            Produto.PrecoCusto = Convert.ToDecimal(txbPrecoCusto.Text.Replace(",","."));
            Produto.PrecoVenda = Convert.ToDecimal(txbPrecoVenda.Text.Replace(",", "."));
            if (Produto.Cod == 0)
            {
                Produto.DataHoraCadastro = DateTime.Now;
            }
            Produto.Ativo = Convert.ToInt32(txbAtivo.IsChecked.Value);
            
            
            return Produto;
        }

        private void BuildUpdate()
        {
            txbDescricao.Text = Produto.Descricao.ToUpper();
            cbbGrupoProduto.SelectedIndex = Produto.CodGrupo-1;
            txbCodigoBarra.Text = Produto.CodBarra;
            txbPrecoCusto.Text = Produto.PrecoCusto.ToString("F");
            txbPrecoVenda.Text = Produto.PrecoVenda.ToString("F");
            if (Produto.Ativo == 0)            
                txbAtivo.IsChecked = false;            
            else
                txbAtivo.IsChecked = true;

            ShowButtons();
        }

        private async void BuildComboBox()
        {
            try
            {
                var response = await new HttpResquestMethods<ProdutoGrupo>().GetAsync();
                grupos = (List<ProdutoGrupo>)response.Data;
                cbbGrupoProduto.ItemsSource = grupos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor Offline");
            }

        }


        private bool IsCadastroValid()
        {
            var message = "";

            if (string.IsNullOrEmpty(txbDescricao.Text))
            {
                message += "Preencher 'Descrição'\n";
            }
            if (cbbGrupoProduto.SelectedIndex == -1)
            {
                message += "Preencher 'Grupo Produto'\n";
            }
            if (!decimal.TryParse(txbPrecoCusto.Text, out decimal number))
            {
                message += "Preencher 'Preço Custo' com um valor Válido\n";
            }
            if (!decimal.TryParse(txbPrecoVenda.Text, out decimal numbers))
            {
                message += "Preencher 'Preço Venda' com um valor Válido\n";
            }

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private void ClearCadastro()
        {
            txbDescricao.Clear();
            cbbGrupoProduto.SelectedIndex = -1;
            txbCodigoBarra.Clear();
            txbPrecoCusto.Clear();
            txbPrecoVenda.Clear();
            txbAtivo.IsChecked = false;
            HiddenButtons();
            Produto = new Produto();
        }

        private void ShowButtons()
        {
            cvCadastro.Visibility=Visibility.Visible;
            cvConsulta.Visibility=Visibility.Hidden;

            btnAtualizar.Visibility = Visibility.Visible;
            btnDeletar.Visibility = Visibility.Visible;
            btnSalvar.Visibility = Visibility.Hidden;
        }

        private void HiddenButtons()
        {
            btnSalvar.Visibility = Visibility.Visible;
            btnAtualizar.Visibility = Visibility.Hidden;
            btnDeletar.Visibility = Visibility.Hidden;
        }
        private async void DataGridViewProdutos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClearCadastro();
            var produto = (VwConsulta)DataGridViewProdutos.SelectedCells[0].Item;
            var id = produto.Cod;

            var result = await new HttpResquestMethods<Produto>().GetByIdAsync(id);
            Produto = (Produto)result.Data;
            if (Produto != null)
            {
                BuildUpdate();
            }
                                                     
        }
    }
}
