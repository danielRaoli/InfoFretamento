using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class CacheManager
    {
        private readonly IMemoryCache _cache;
        private  List<string> _cacheKeys = new();

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        // Adiciona uma chave à lista
        public void AddKey(string key)
        {
            if (!_cacheKeys.Contains(key))
            {
                _cacheKeys.Add(key);
            }
        }


        // Limpa todas as chaves armazenadas
        public void ClearAll(string cacheKey)
        {
            // Filtra as chaves que contêm o prefixo fornecido
            var cacheKeys = _cacheKeys.Where(k => k.Contains(cacheKey)).ToList();

            // Remove do cache as chaves filtradas
            foreach (var key in cacheKeys)
            {
                _cache.Remove(key);
            }

            // Agora remove apenas as chaves que foram realmente removidas do cache
            _cacheKeys = _cacheKeys.Where(k => !k.Contains(cacheKey)).ToList();
        }
    }
}
