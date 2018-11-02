using System;
using Amazon.DynamoDBv2.DocumentModel;
using Lambda.Common.Extensions;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Utils
{
    /// <summary>
    /// Builds AWS Dynamo Document
    /// </summary>
    public class DocumentBuilder : IDocumentBuilder
    {
        private Document _document;

        public DocumentBuilder()
        {
            _document = new Document();
        }

        /// <summary>
        /// Adds all properties of object to top-level document
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DocumentBuilder CreateDocumentFrom(object obj)
        {
            var doc = Document.FromJson(obj.ToJson());

            foreach (var attr in doc)
                _document[attr.Key] = attr.Value;

            return this;
        }

        /// <summary>
        /// Adds serialized object to document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DocumentBuilder AddObject(string key, object obj)
        {
            _document[key] = Document.FromJson(obj.ToJson());
            return this;
        }

        /// <summary>
        /// Adds string value to document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DocumentBuilder AddProperty(string key, string value)
        {
            _document[key] = value;
            return this;
        }

        /// <summary>
        /// Adds DateTime as ISO-8601 string to document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DocumentBuilder AddDateTime(string key, DateTime dateTime)
        {
            _document[key] = dateTime.ToString("O");
            return this;
        }

        /// <summary>
        /// Outputs internal document
        /// </summary>
        /// <returns></returns>
        public Document Build() => this._document;

        /// <summary>
        /// Clears internal document
        /// </summary>
        public void Reset() => this._document = new Document();
    }
}
