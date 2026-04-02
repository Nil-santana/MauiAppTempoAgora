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
                    
                        // Verifica conexão com a internet antes de buscar
                        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                        {
                            await DisplayAlert("Sem conexão",
                                "Você está sem conexão com a internet." +
                                "Verifique sua rede e tente novamente.",
                                "OK");
                            return;
                        }
                        Tempo? t = await DataService.Gatprevisao(txt_Cidade.Text);

                        if (t != null)
                        {

                            string dados_previsao = " ";


                            dados_previsao = $"Latitude: {t.lat} \n " +
                                             $"Longitude: {t.lon} \n " +
                                             $"Nascer do sol: {t.sunrise} \n " +
                                             $"Pôr do sol: {t.sunset} \n " +
                                             $"Temp.Máx: {t.temp_max}°C \n " +
                                             $"Temp.min: {t.temp_min}°C \n " +
                                             $"Clima: {t.description} \n " +
                                             $"Velocidade do vento: {t.speed} km/h \n " +
                                             $"Visibilidade: {t.visibility} m \n ";

                            lbl_res.Text = dados_previsao;
                        }
                        else
                        {

                            lbl_res.Text = "Cidade Não Encontrada";
                        }
                    }
                    else
                    {
                        lbl_res.Text = "preencha o nome da cidade";
                    }
            }
            //captura erro de rede/conexão separadamente
            catch (HttpRequestException)
            {
                await DisplayAlert("Erro de conexão",
                    "Não foi possível conectar ao servidor. Verifique sua internet.",
                    "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}