using Core.Abstractions;

namespace CompanyManagement.Service.Domain.DataModels.Base
{
    internal abstract class BaseDataModel : IBaseDataModel, IBase<Guid>
    {
        /// <summary>Stores Id of the Object</summary>
        public Guid Id { get; set; }

        /// <inheritdoc />
        /// <summary>Stores the time when object was created</summary>
        public DateTime Created { get; set; }

        /// <inheritdoc />
        /// <summary>Stores the time when object was modified. Nullable</summary>
        public DateTime Changed { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Stores state of the Object. True if object is deleted and false otherwise
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Version of data
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// Get props name
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetPropsName()
        {
            return typeof(BaseDataModel).GetProperties().Select(x => x.Name).ToList();
        }
    }
}
