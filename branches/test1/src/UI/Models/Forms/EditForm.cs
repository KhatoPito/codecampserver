using System;
using July09v31.Core;
using July09v31.UI.Helpers.Mappers;

namespace July09v31.UI.Models.Forms
{
    public abstract class EditForm<T> : ValueObject<T> where T : class
    {
        public abstract Guid Id { get; set; }

        public virtual EditMode GetEditMode()
        {
            if (Guid.Empty.Equals(Id))
            {
                return EditMode.Add;
            }

            return EditMode.Edit;
        }
    }
}