using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace LoLStats
{
    public class Api
    {
        /*
         *  This class contains functions used to communicate with
         *  the RIOT Games API. In order to work properly, active
         *  and fully working API key is required. This product is
         *  not yet registered, thus uses API key only for development.
         *  Development keys last only 24 hours since its generation.
         * 
         */
        private string apiKey = "RGAPI-e55d7117-c9b0-48b4-b6d6-52538bfef8a2"; //generated 6/1/2021 14:38 CET 

        protected HttpResponseMessage responseMessage(string url)
        {
            try
            {
                using (HttpClient APIclient = new HttpClient())
                {
                    var response = APIclient.GetAsync(url);
                    response.Wait();
                    return response.Result;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public List<LeagueInfoModel> GetLeagueInfoBySummonerID(string region, string summonerId)
        {
            string urlPath = "https://" + region + ".api.riotgames.com/lol/league/v4/entries/by-summoner/" + summonerId + "?api_key=" + apiKey;
            var response = responseMessage(urlPath);
            Debug.WriteLine("League info model: " + response.Content.ReadAsStringAsync().Result);
            if (System.Net.HttpStatusCode.OK == response.StatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<LeagueInfoModel>>(response.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                return null;
            }
            else
            {
                Debug.WriteLine("Response status code is not OK");
            }
            return null;
        }
        public List<ChampionModel> GetChampionsBySummonerID(string region, string summonerId)
        {
            string urlPath = "https://" + region + ".api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/" + summonerId + "?api_key=" + apiKey;
            var response = responseMessage(urlPath);
            Debug.WriteLine("Champion model: " + response.Content.ReadAsStringAsync().Result);
            if (System.Net.HttpStatusCode.OK == response.StatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<ChampionModel>>(response.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                return null;
            }
            else
            {
                Debug.WriteLine("Response status code is not OK");
            }
            return null;
        }
        public SummonerModel GetSummonerByName(string region, string summonerName)
        {
            string urlPath = "https://" + region + ".api.riotgames.com/lol/summoner/v4/summoners/by-name/" + summonerName + "?api_key=" + apiKey;
            var response = responseMessage(urlPath);
            Debug.WriteLine("Summoner model info" + response.Content.ReadAsStringAsync().Result);
            if(System.Net.HttpStatusCode.OK == response.StatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<SummonerModel>(response.Content.ReadAsStringAsync().Result);
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                return null;
            }
            else
            {
                Debug.WriteLine("Response status code is not OK");
                return null;
            }
        }
    }
}
