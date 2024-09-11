using System;

namespace StudyHub.Common.Req
{
    public class BaseModel
    {
        #region -- Enums --

        public enum RecordStatus
        {
            Active,
            Inactive,
            Archived
        }

        #endregion

        #region -- Constructors --

        /// <summary>
        /// Initialize the base model with an ID.
        /// </summary>
        /// <param name="id">ID of the model</param>
        public BaseModel(int id)
        {
            Id = id;
            Status = RecordStatus.Active; // Default to Active unless specified otherwise
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Unique identifier for the model.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Current status of the model.
        /// </summary>
        public RecordStatus Status { get; set; }

        /// <summary>
        /// User ID of the person who created the model.
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Date and time the model was created.
        /// </summary>
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// User ID of the person who last modified the model.
        /// </summary>
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// Date and time the model was last modified.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        #endregion
    }
}
