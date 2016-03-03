using System;
using System.Data;

namespace Gremlins.DataAccess
{
    static class DbCommandExtension
    {
        /// <summary>
        /// Add parameter to specified command
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>        
        public static void AddParameter(this IDbCommand cmd, InputParameter inputParameter)
        {
            // Guard input arguments
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));
            if (inputParameter == null)
                throw new ArgumentNullException(nameof(inputParameter));

            // Create DbParameter            
            IDbDataParameter parameter = cmd.CreateParameter();

            // Specify parameter name
            parameter.ParameterName = inputParameter.Name;
            
            // if we have not factory try to use general logic
            if (inputParameter.Configuration == null)
                ApplyDefaultConfiguration(parameter, inputParameter.Value);
            // if we have factory then construct value via factory
            else
                inputParameter.Configuration.Apply(parameter, inputParameter.Value);

            // Add parameters to command
            cmd.Parameters.Add(parameter);            
        }

        private static void ApplyDefaultConfiguration(IDbDataParameter parameter, object value)
        {
            //if (paramValue is DateTime)
            //{
            //    if ((DateTime)paramValue < SQLMinDate)
            //        paramValue = SQLMinDate;
            //}

            //if (value is Guid)
            //    value = ((Guid)value).ToString().ToUpper();                        

            //if (paramValue is XmlDocument)
            //{
            //    parameter.DbType = System.Data.DbType.Xml;
            //    paramValue = (paramValue as XmlDocument).OuterXml;
            //}            

            if (value == null)
                value = DBNull.Value;

            parameter.Value = value;
        }
    }
}
