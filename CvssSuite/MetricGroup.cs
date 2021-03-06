﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Cvss.Suite
{
    internal abstract class MetricGroup
    {
        internal MetricGroup(Dictionary<string, string> metrics, string notDefined = null)
        {
            ExtractedMetrics = metrics;
            NotDefined = notDefined;
        }

        protected List<Metric> AvailableMetrics;

        internal abstract double Score();

        internal string SelectedValue(string metric)
        {

            try
            {
                var selectedMetric = AvailableMetrics.Single(item => item.Name == metric);

                return selectedMetric.Values.Single(item => item.Abbreviation == ExtractedMetrics[selectedMetric.Abbreviation]).Name;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        protected double MetricScore(Metric metric)
        {
            var metricAbbreviation = metric.Abbreviation;

            if (ExtractedMetrics.ContainsKey(metricAbbreviation) && ExtractedMetrics[metricAbbreviation] == NotDefined)
            {
                switch (metricAbbreviation)
                {
                    case "MAV":
                        metricAbbreviation = "AV";
                        break;
                    case "MAC":
                        metricAbbreviation = "AC";
                        break;
                    case "MPR":
                        metricAbbreviation = "PR";
                        break;
                    case "MUI":
                        metricAbbreviation = "UI";
                        break;
                    case "MS":
                        metricAbbreviation = "S";
                        break;
                    case "MC":
                        metricAbbreviation = "C";
                        break;
                    case "MI":
                        metricAbbreviation = "I";
                        break;
                    case "MA":
                        metricAbbreviation = "A";
                        break;
                }
            }

            if (ExtractedMetrics.ContainsKey(metricAbbreviation))
            {
                return metric.Values.Single(item => item.Abbreviation == ExtractedMetrics[metricAbbreviation]).Score;
            }
            else
            {
                return metric.Values.Single(item => item.Abbreviation == NotDefined).Score;
            }
        }

        protected Dictionary<string, string> ExtractedMetrics;

        protected Dictionary<string, double> MetricValues = new Dictionary<string, double>();

        private string NotDefined;

    }
}
