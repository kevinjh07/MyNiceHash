using MyNiceHash.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MyNiceHash.Helpers;

namespace MyNiceHash.Rest {
    public class RestRepository : IRestRepository {
        private readonly string APIVersionUrl = "https://api.nicehash.com/api";

        private readonly string GetProviderWokersUrl = "https://api.nicehash.com/api?method=stats.provider.workers&addr={0}&algo={1}";

        private readonly string GetProviderStatsUrl = "https://api.nicehash.com/api?method=stats.provider&addr={0}";

        public async Task<string> GetAPIVersoin() {
            var version = string.Empty;
            using (var client = new HttpClient()) {
                var json = await client.GetStringAsync(APIVersionUrl);
                if (json == null) {
                    return null;
                }
                var reader = new JsonTextReader(new StringReader(json));
                while (reader.Read()) {
                    if (!"api_version".Equals(reader.Value)) {
                        continue;
                    }
                    reader.Read();
                    version = (string)reader.Value;
                }
            }
            return version;
        }


        public async Task<List<Worker>> GetWorkers(AlgorithmType algorithm) {
            var workers = new List<Worker>();
            using (var client = new HttpClient()) {
                var json = await client.GetStringAsync(string.Format(GetProviderWokersUrl, Settings.BtcAddress, (int)algorithm));
                if (json == null) {
                    return null;
                }
                var reader = new JsonTextReader(new StringReader(json));
                var lastWorker = false;
                while (reader.Read()) {
                    if (!"workers".Equals(reader.Value)) {
                        continue;
                    }
                    while (!lastWorker) {
                        var worker = new Worker();
                        while (reader.TokenType != JsonToken.String) {
                            reader.Read();
                            if (reader.TokenType == JsonToken.EndArray) {
                                return null;
                            }
                        }
                        worker.Name = string.Format("{0}", reader.Value);

                        while (reader.TokenType != JsonToken.StartObject) {
                            reader.Read();
                        }
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndObject) {
                            if (reader.TokenType == JsonToken.PropertyName) {
                                if ("a".Equals(reader.Value)) {
                                    reader.Read();
                                    worker.AcceptedSpeed = string.Format("{0}", reader.Value);
                                }
                                if ("rt".Equals(reader.Value)) {
                                        reader.Read();
                                    worker.RejectedTargetSpeed = string.Format("{0}", reader.Value);
                                }
                                if ("rs".Equals(reader.Value)) {
                                    reader.Read();
                                    worker.RejectedStaleSpeed = string.Format("{0}", reader.Value);
                                }
                                if ("rd".Equals(reader.Value)) {
                                    reader.Read();
                                    worker.RejectedDuplicateSpeed = string.Format("{0}", reader.Value);
                                }
                                if ("ro".Equals(reader.Value)) {
                                    reader.Read();
                                    worker.RejectedOtherSpeed = string.Format("{0}", reader.Value);
                                }
                            }
                            reader.Read();
                            continue;
                        }

                        reader.Read();
                        worker.TimeConnected = string.Format("{0}", reader.Value);

                        reader.Read();
                        worker.XnSub = string.Format("{0}", reader.Value);

                        reader.Read();
                        worker.Difficulty = string.Format("{0}", reader.Value);

                        reader.Read();
                        worker.Location = string.Format("{0}", reader.Value);

                        workers.Add(worker);
                        while (reader.TokenType != JsonToken.EndArray) {
                            reader.Read();
                        }
                        reader.Read();
                        lastWorker = (reader.TokenType == JsonToken.EndArray);
                    }
                    break;
                }
            }
            return workers;
        }

        public async Task<List<ProviderStats>> GetProviderStats() {
            var providerStats = new List<ProviderStats>();
            using (var client = new HttpClient()) {
                var json = await client.GetStringAsync(string.Format(GetProviderStatsUrl, Settings.BtcAddress));
                if (json == null) {
                    return null;
                }
                var reader = new JsonTextReader(new StringReader(json));
                while (reader.Read()) {
                    System.Diagnostics.Debug.WriteLine(reader.TokenType);
                    System.Diagnostics.Debug.WriteLine(reader.Value);
                    if (!"stats".Equals(reader.Value)) {
                        continue;
                    }
                    while (reader.Read()) {
                        if (reader.TokenType == JsonToken.EndArray) {
                            break;
                        }
                        if (reader.TokenType != JsonToken.PropertyName) {
                            continue;
                        }
                        var stats = new ProviderStats();
                        if ("balance".Equals(reader.Value)) {
                            reader.Read();
                            stats.Balance = (string)reader.Value;
                        }
                        reader.Read();
                        if ("rejected_speed".Equals(reader.Value)) {
                            reader.Read();
                            stats.RejectedSpeed = (string)reader.Value;
                        }
                        reader.Read();
                        if ("algo".Equals(reader.Value)) {
                            reader.Read();
                            stats.Algorithm = (AlgorithmType)(int.Parse(reader.Value.ToString()));
                        }
                        reader.Read();
                        if ("accepted_speed".Equals(reader.Value)) {
                            reader.Read();
                            stats.AcceptedSpeed = (string)reader.Value;
                        }
                        providerStats.Add(stats);
                    }
                }
            }
            return providerStats;
        }
    }
}
