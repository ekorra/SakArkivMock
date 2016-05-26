using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EduBestServiceStub.Lib.NoarkTypes;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace EduBestServiceStub.Lib
{
    public class DocumentDbRequestLogger: IRequestLogger
    {
        private DocumentClient client;

        public DocumentDbRequestLogger(DocumentClient documentClient)
        {
            client = documentClient;
        }

        public async void Log(PutMessageRequestType request)
        {
            var logid = $"{request.envelope.sender.orgnr}_{DateTime.Now.GetTimestamp()}";
            request.Id = logid;
            // CreateRequestDocumentIfNotExists()
            try
            {
                var a = client.ReadEndpoint;
                //var uri = UriFactory.CreateCollectionUri("messagedb", "bestedumessages");
                var uri = UriFactory.CreateDocumentCollectionUri(Resource.DocumentDb_DatabaseId, Resource.DocumentDb_CollectionId);
                var res = Task.Run(() => client.ReadDocumentCollectionAsync(uri)).Result;
                var db = Resource.DocumentDb_DatabaseId;
                Database database = client.CreateDatabaseQuery().Where(d => d.Id == Resource.DocumentDb_DatabaseId).AsEnumerable().FirstOrDefault();
                var dot = client.CreateDocumentCollectionQuery((string)database.SelfLink).ToList();
                foreach (var collection in dot)
                {
                    //var id = collection.Id
                    string id = string.Empty;
                    var documents = client.CreateDocumentQuery(collection.DocumentsLink);
                    foreach (var doc in documents)
                    {
                        Debug.WriteLine(doc.Id);
                        id = doc.Id;
                        break;
                        //var duri = UriFactory.CreateDocumentUri("messagedb", "bestedumessages", doc.Id);
                        //var dok = Task.Run(() => client.ReadDocumentAsync(duri)).Result;
                    }

                    FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
                    IQueryable<PutMessageRequestType> familyQuery = this.client.CreateDocumentQuery<PutMessageRequestType>(
                UriFactory.CreateDocumentCollectionUri(Resource.DocumentDb_DatabaseId, Resource.DocumentDb_CollectionId), queryOptions).Where(f => f.Id == id);
                    var noe = familyQuery.AsEnumerable().FirstOrDefault();

                }
                
                

                var result = Task.Run(() => client.CreateDocumentAsync(uri, request)).Result;
                var documentUri = UriFactory.CreateDocumentUri(Resource.DocumentDb_DatabaseId, Resource.DocumentDb_CollectionId, request.Id);
                var document = Task.Run(() => client.ReadDocumentAsync(documentUri)).Result;
                

                //var ress = Task.Run(() => client.ReadDatabaseAsync()).Result;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            //await Task.Run(() => CreateRequestDocumentIfNotExists("messagedb", "bestedumessages", request));
        }


        private async Task CreateRequestDocumentIfNotExists(string databaseName, string collectionName, PutMessageRequestType request)
        {
            try
            {
                await client.CreateDocumentAsync(
                            UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), request);

                //client.ReadDocumentCollectionAsync()
                //await
                //        client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName,
                //            request.Id));

            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await
                        client.CreateDocumentAsync(
                            UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), request);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var noe = ex.Message;
            }
        }
    }
}
