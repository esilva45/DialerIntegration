using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows.Forms;

namespace DialerIntegration {
    public partial class Campanha : Form {
        string Url = string.Empty;

        public Campanha() {
            InitializeComponent();
        }

        public Campanha(string url_number) {
            InitializeComponent();
            Url = url_number;
            Load();
        }

        private new void Load() {
            try {
                var json = new WebClient().DownloadString(Url);
                dynamic array = JsonConvert.DeserializeObject(json);

                textBox1.Text = array.camp;
                textBox2.Text = array.tel;
                textBox3.Text = array.nome;
                textBox4.Text = array.cpf;
                textBox5.Text = array.credor;
                textBox6.Text = array.email;
            }
            catch (Exception) { }
        }
    }
}
