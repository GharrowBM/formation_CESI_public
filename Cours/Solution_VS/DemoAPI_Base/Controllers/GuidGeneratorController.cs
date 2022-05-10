using DemoAPI_Base.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI_Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidGeneratorController : ControllerBase
    {
        private readonly IGuidGeneratorService transientGeneratorA;
        private readonly IGuidGeneratorService transientGeneratorB;
        private readonly IGuidGeneratorServiceBis scopedGeneratorA;
        private readonly IGuidGeneratorServiceBis scopedGeneratorB;
        private readonly IGuidGeneratorServiceTer singletonGeneratorA;
        private readonly IGuidGeneratorServiceTer singletonGeneratorB;

        public GuidGeneratorController(IGuidGeneratorService transientGeneratorA, IGuidGeneratorService transientGeneratorB, 
            IGuidGeneratorServiceBis scopedGeneratorA, IGuidGeneratorServiceBis scopedGeneratorB, 
            IGuidGeneratorServiceTer singletonGeneratorA, IGuidGeneratorServiceTer singletonGeneratorB)
        {
            this.transientGeneratorA = transientGeneratorA;
            this.transientGeneratorB = transientGeneratorB;
            this.scopedGeneratorA = scopedGeneratorA;
            this.scopedGeneratorB = scopedGeneratorB;
            this.singletonGeneratorA = singletonGeneratorA;
            this.singletonGeneratorB = singletonGeneratorB;
        }

        [HttpGet]
        public IActionResult TestGuidGenerated()
        {
            /*
             * On peut ici observer que les Guid générées seront :
             * - Différente entre les deux Transient peut importe la requête
             * - Sembable pour les deux ScopeD au sein de la même requête mais différente à chaque nouvelle requête
             * - Semblable pour les deux Singleton durant tout le fonctionnement de l'application
             * 
             */

            return Ok(new
            {
                TransientGeneratedA = transientGeneratorA.GuidGenerated,
                TransientGeneratedB = transientGeneratorB.GuidGenerated,
                ScopedGeneratedA = scopedGeneratorA.GuidGenerated,
                ScopedGeneratedB = scopedGeneratorB.GuidGenerated,
                SingletonGeneratedA = singletonGeneratorA.GuidGenerated,
                SingletonGeneratedB = singletonGeneratorB.GuidGenerated,
            });
        }
    }
}
