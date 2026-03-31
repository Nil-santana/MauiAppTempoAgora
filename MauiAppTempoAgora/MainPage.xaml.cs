using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_Cidade.Text))
                {
                    Tempo? t = await DataService.Gatprevisao(txt_Cidade.Text);

                    if (t != null)
                    {

                        string dados_previsao = " ";

                        lbl_res.Text = dados_previsao;

                        dados_previsao = $"Latitude: {t.lat} \n " +
                                         $"Longitude: {t.lon} \n " +
                                         $"Nascer do sol: {t.sunrise} \n " +
                                         $"Pôr do sol: {t.sunset} \n " +
                                         $"Temp.Máx: {t.temp_max} \n " +
                                         $"Temp.min: {t.temp_min} \n ";
                    }
                    else
                    {

                        lbl_res.Text = "Sem dados de previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "preencha a cidade";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}