using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json;
using NotSpotify.MVVM.Model;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace NotSpotify.MVVM.ViewModel;

public class MainViewModel
{
    public ObservableCollection<Item> Songs { get; set; }
    public MainViewModel()
    {
        Songs = new ObservableCollection<Item>();
        populateCollection();
    }

    public void populateCollection()
    {
        var client = new RestClient();
        client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQCiERqLgFc3CZ7LcXqAu8Z0WqU-HQmjlr7gkp59jPKmVs9_xLvtOhQE-zK81ntyghCTkqsmYHa94Wk8jxzcuRDK_wuMqwwv-dEA09xAD-LhREeIjvXPDEdOaFu3wmopZhyMphAjAgGBoF-dgkmGktM5BKMUJ16fqMCmt3Qq57G5R_wG-Q", "Bearer");

        var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        var response = client.GetAsync(request).GetAwaiter().GetResult();
        var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

        for (int i = 0; i < data.Albums.Limit; i++)
        {
            var track = data.Albums.Items[i];
            track.Duration = "2:32";
            Songs.Add(track);
        }

    }
}