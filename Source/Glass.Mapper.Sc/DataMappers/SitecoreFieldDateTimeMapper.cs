using System;

namespace Glass.Mapper.Sc.DataMappers
{
    /// <summary>
    /// Class SitecoreFieldDateTimeMapper
    /// </summary>
    public class SitecoreFieldDateTimeMapper : AbstractSitecoreFieldMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SitecoreFieldDateTimeMapper"/> class.
        /// </summary>
        public SitecoreFieldDateTimeMapper():
            base(typeof(DateTime))
        {

        }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <param name="fieldValue">The field value.</param>
        /// <param name="config">The config.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.Object.</returns>
        public override object GetFieldValue(string fieldValue, Configuration.SitecoreFieldConfiguration config, SitecoreDataMappingContext context)
        {
            var isoDate = Sitecore.DateUtil.IsoDateToDateTime(fieldValue);
#if (SC80 || SC81 || SC82 || SC90)
            return fieldValue.EndsWith("Z", StringComparison.OrdinalIgnoreCase) 
                ? Sitecore.DateUtil.ToServerTime(isoDate) 
                : isoDate;
#else
            return isoDate;
#endif
        }

        /// <summary>
        /// Sets the field value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="config">The config.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.NotSupportedException">The value is not of type System.DateTime</exception>
        public override string SetFieldValue(object value, Configuration.SitecoreFieldConfiguration config, SitecoreDataMappingContext context)
        {
            if (value is DateTime)
            {
                DateTime date = (DateTime)value;

#if (SC80 || SC81 || SC82 || SC90)
                return global::Sitecore.DateUtil.ToIsoDate(date, false, true);
#else
                return global::Sitecore.DateUtil.ToIsoDate(date);
#endif
            }
            else
                throw new NotSupportedException("The value is not of type System.DateTime");
        }
    }
}




