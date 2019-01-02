using MyNiceHash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNiceHash.Services {
    public interface IProviderStatsService {
        Task<List<ProviderStats>> GetProviderStats();
    }
}
