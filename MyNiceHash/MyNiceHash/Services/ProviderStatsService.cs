using System.Collections.Generic;
using MyNiceHash.Models;
using MyNiceHash.Rest;
using System.Threading.Tasks;

namespace MyNiceHash.Services {
    class ProviderStatsService : IProviderStatsService {
        private readonly IRestRepository _restRepository;

        public ProviderStatsService(IRestRepository restRepository) {
            _restRepository = restRepository;
        }

        public async Task<List<ProviderStats>> GetProviderStats() {
            return await _restRepository.GetProviderStats();
        }
    }
}
