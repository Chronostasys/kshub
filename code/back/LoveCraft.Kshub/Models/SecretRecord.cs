using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public static class KshubScretRecord
    {
        public static IServiceCollection AddSecrertRecord<T>(this IServiceCollection collection,Action<SecretRecord> options)
            where T:ISecretRecord
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (options == null) throw new ArgumentNullException(nameof(options));
            collection.Configure<SecretRecord>(options);
            return collection.AddSingleton<SecretRecord>();
        }
    }
    public class SecretRecord:ISecretRecord
    {
        public SecretRecord(IOptions<SecretRecord> options)
            : this(options.Value.GrillenPassword) { }
        public SecretRecord(string password)
        {
            GrillenPassword = password;
        }
        public SecretRecord() { }
        public string GrillenPassword { get; set; }
    }
    public interface ISecretRecord
    {
        public string GrillenPassword { get; set; }

    }
}
