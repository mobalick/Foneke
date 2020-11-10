using System.Configuration;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using EMM.FoNeke.Common.Entities;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Models.Enums;
using FoNeke.Web.Repositories.Interfaces;

namespace FoNeke.Web.Repositories.Implementations
{
    public class AttachementRepository : BaseRepository<Attachement>, IAttachementRepository
    {
        private readonly MongoGridFS _gridFs;

        public AttachementRepository()
        {
            var url = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString);
            var client = new MongoClient(url);
            var server = client.GetServer();
            _gridFs = server.GetDatabase(url.DatabaseName).GridFS;
        }


        public new async Task<Attachement> SaveOrUpdate(Attachement entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.FileId.ToString()))
            {
                _gridFs.DeleteById(entity.FileId);
            }
            var fileInfo = _gridFs.Upload(entity.Stream, entity.Name);
            await entity.Stream.FlushAsync();
            entity.Stream = null;
            entity.FileId = (ObjectId) fileInfo.Id;

            return await base.SaveOrUpdate(entity);
        }

        public new Attachement GetById(string id)
        {
            var file = _gridFs.FindOneById(id);
            var attachement = base.GetById(id);
            attachement.File = file;

            return attachement;
        }

    }
}