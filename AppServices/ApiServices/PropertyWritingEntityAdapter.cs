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
using Fourspace.Toolbox.Service;

namespace FourSpace.AppServices.ApiServices
{
    /// <summary>
    /// Uses property writer to adapt an entity
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="O"></typeparam>
    public class PropertyWritingEntityAdapter<I, C, O> : IAdapter<I, C, O>
        where O : new()
    {
        private readonly IPropertyWriter<O, I, C> propertyWriter;

        public PropertyWritingEntityAdapter(IPropertyWriter<O, I, C> propertyWriter)
        {
            this.propertyWriter = propertyWriter;
        }

        public O Adapt(I item, C context)
        {
            O to = new O();
            propertyWriter.WriteProperties(to, item, context, false);
            return to;
        }
    }

    /// <summary>
    /// Uses property writer to adapt an entity
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="O"></typeparam>
    public class PropertyWritingEntityAdapter<I, O> : IAdapter<I, O>
        where O : new()
    {
        private readonly IPropertyWriter<O, I, object> propertyWriter;

        public PropertyWritingEntityAdapter(IPropertyWriter<O, I, object> propertyWriter)
        {
            this.propertyWriter = propertyWriter;
        }

        public O Adapt(I item)
        {
            O to = new O();
            propertyWriter.WriteProperties(to, item, null, false);
            return to;
        }
    }

}
