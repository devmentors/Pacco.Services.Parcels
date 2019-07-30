using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pacco.Services.Parcels.Core.Entities;
using Pacco.Services.Parcels.Infrastructure.Mongo.Documents;
using Pacco.Services.Parcels.PactProviderTests.Fixtures;
using Pacco.Services.Parcels.PactProviderTests.Outputters;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace Pacco.Services.Parcels.PactProviderTests.PACT
{
    public class ParcelsApiPactProviderTests : IDisposable
    {
        [Fact]
        public async Task Pact_Should_Be_Verified()
        {
            await _mongoDbFixture.InsertAsync(Parcel);
            
            var pactConfig = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutputter(_output),
                },
                Verbose = true
            };

            new PactVerifier(pactConfig)
                .ServiceProvider("parcels", "http://localhost:5007")
                .HonoursPactWith("orders")
                .PactUri(@"..\..\..\..\..\..\pacts\orders-parcels.json")
                .Verify();
        }

        #region ARRANGE

        private readonly ParcelDocument Parcel = new ParcelDocument
        {
            Id =  new Guid("c68a24ea-384a-4fdc-99ce-8c9a28feac64"),
            Name = "Product",
            Size = Size.Huge,
            Variant = Variant.Weapon
        };
        
        private readonly ITestOutputHelper _output;
        private readonly MongoDbFixture<ParcelDocument, Guid> _mongoDbFixture;
        private bool _disposed = false;

        public ParcelsApiPactProviderTests(ITestOutputHelper output)
        {
            _output = output;
            _mongoDbFixture = new MongoDbFixture<ParcelDocument, Guid>("test-parcels-service", "Parcels");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _mongoDbFixture.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}