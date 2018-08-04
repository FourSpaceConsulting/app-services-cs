/*
MIT License

Copyright (c) 2017 Richard Steward

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using Fourspace.Toolbox.DataSource;
using Fourspace.Toolbox.Service;
using Fourspace.Validator;
using FourSpace.AppServices.ApiServices;
using System.Collections.Generic;
using System.Linq;

namespace Fourspace.AppServices.ApiServices
{
    public class EntityUpdater<IT, C, OT> : IEntityUpdater<IT, C, OT>
        where OT : class, new()
    {
        private readonly IExistingItemLookup<IT, OT> existingItemLookup;
        private readonly IPropertyWriter<OT, IT, C> propertyWriter;
        private readonly IValidator<ValidationEntity<OT,IT,C>> validator;
        private readonly IUpdatableSource<OT> updatableSource;

        public OT Update(IT obj, C context)
        {
            OT existingItem = existingItemLookup.GetExistingItem(obj);
            OT updated = WriteItem(existingItem, obj, context);
            return updatableSource.Save(updated);
        }

        public IReadOnlyList<OT> Update(IEnumerable<IT> objs, C context)
        {
            var existingItem = existingItemLookup.GetExistingItems(objs);
            var updated = existingItem.Select(i => WriteItem(i.Second, i.First, context));
            return updatableSource.Save(updated);
        }

        private OT WriteItem(OT existingItem, IT from, C context)
        {
            string itemId = "";
            validator.Validate(itemId, new ValidationEntity<OT, IT, C>() { toUpdate = existingItem, newItem = from, context = context });
            OT updated = existingItem ?? new OT();
            propertyWriter.WriteProperties(updated, from, context, true);
            return updated;
        }
    }
}
