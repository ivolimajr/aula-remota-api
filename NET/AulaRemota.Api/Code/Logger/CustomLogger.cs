using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace AulaRemota.Api.Code.Logger
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;
        private readonly string _basePath;
        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            _basePath = Directory.GetCurrentDirectory() + "\\Code\\Logger\\Logs\\";
            this.loggerName = name;
            loggerConfig = config;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = string.Format("{0}: {1} - {2}", logLevel.ToString(),
                eventId.Id, formatter(state, exception));
            EscreverTextoNoArquivo(mensagem);
        }
        private void EscreverTextoNoArquivo(string mensagem)
        {
            var destino = Path.Combine(_basePath);
            string caminhoArquivoLog = _basePath+"Aula_Remota_Api_Log.txt";
            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            }
        }
    }
}