using System;
using Amazon.DynamoDBv2.DocumentModel;
using Lambda.Common.Utils;

namespace Lambda.Common.Interfaces
{
    public interface IDocumentBuilder
    {
        /// <summary>
        /// Adds all properties of object to top-level document
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        DocumentBuilder CreateDocumentFrom(object obj);

        /// <summary>
        /// Adds serialized object to document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        DocumentBuilder AddObject(string key, object obj);

        /// <summary>
        /// Adds string value to document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        DocumentBuilder AddProperty(string key, string value);

        /// <summary>
        /// Adds DateTime as ISO-8601 string to document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        DocumentBuilder AddDateTime(string key, DateTime dateTime);

        /// <summary>
        /// Outputs internal document
        /// </summary>
        /// <returns></returns>
        Document Build();

        /// <summary>
        /// Clears internal document
        /// </summary>
        void Reset();
    }
}