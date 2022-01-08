using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApiCatalogosJogos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeCycleIDController : ControllerBase
    {
        public readonly IExampleSingleton _exampleSingleton1;
        public readonly IExampleSingleton _exampleSingleton2;

        public readonly IExampleScoped _exampleScoped1;
        public readonly IExampleScoped _exampleScoped2;

        public readonly IExampleTransient _exampleTransient1;
        public readonly IExampleTransient _exampleTransient2;

        public LifeCycleIDController(IExampleSingleton exampleSingleton1,
                                    IExampleSingleton exampleSingleton2,
                                    IExampleScoped exampleScoped1,
                                    IExampleScoped exampleScoped2,
                                    IExampleTransient exampleTransient1,
                                    IExampleTransient exampleTransient2)
        {

            _exampleSingleton1 = exampleSingleton1;
            _exampleSingleton2 = exampleSingleton2;
            _exampleScoped1 = exampleScoped1;
            _exampleScoped2 = exampleScoped2;
            _exampleTransient1 = exampleTransient1;
            _exampleTransient2 = exampleTransient2;
        }

        [HttpGet]
        public Task<String> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_exampleSingleton1}");
            stringBuilder.AppendLine($"Singleton 2: {_exampleSingleton2}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_exampleScoped1}");
            stringBuilder.AppendLine($"Scoped 2: {_exampleScoped2}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_exampleTransient1}");
            stringBuilder.AppendLine($"Transient 1: {_exampleTransient2}");

            return Task.FromResult(stringBuilder.ToString());
        }

        public interface IExampleGeneral
        {
            public Guid Id { get; }
        }

        public interface IExampleSingleton : IExampleGeneral
        { }

        public interface IExampleScoped : IExampleGeneral
        { }

        public interface IExampleTransient : IExampleGeneral
        { }



    }
}
