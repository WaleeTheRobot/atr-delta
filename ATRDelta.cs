#region Using declarations
using NinjaTrader.Gui;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;
#endregion

namespace NinjaTrader.NinjaScript.Indicators
{
    public class ATRDelta : Indicator
    {
        public const string GROUP_NAME_GENERAL = "1. General";
        public const string GROUP_NAME_ATR_DELTA = "2. ATR Delta";
        public const string GROUP_NAME_PLOTS = "Plots";

        private ATR _atr;
        private ATR _fastATR;

        #region General Properties

        [NinjaScriptProperty]
        [Display(Name = "Version", Description = "ATR Delta version.", Order = 0, GroupName = GROUP_NAME_GENERAL)]
        [ReadOnly(true)]
        public string Version => "1.0.0";

        #endregion

        #region ATR Delta Properties

        [NinjaScriptProperty]
        [Display(Name = "Fast Period", GroupName = GROUP_NAME_ATR_DELTA, Order = 0)]
        public int FastPeriod { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Period", GroupName = GROUP_NAME_ATR_DELTA, Order = 1)]
        public int Period { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Override Plot Colors", GroupName = GROUP_NAME_ATR_DELTA, Order = 2)]
        public bool OverridePlotColors { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Custom Positive Color", GroupName = GROUP_NAME_ATR_DELTA, Order = 3)]
        [XmlIgnore]
        public Brush CustomPositiveColor { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Custom Negative Color", GroupName = GROUP_NAME_ATR_DELTA, Order = 4)]
        [XmlIgnore]
        public Brush CustomNegativeColor { get; set; }

        #endregion

        #region Plots Properties

        [Browsable(false)]
        [XmlIgnore]
        [Display(Name = "ATR Delta", GroupName = GROUP_NAME_PLOTS, Order = 0)]
        public Series<double> ATRDeltaSeries
        {
            get { return Values[0]; }
        }

        #endregion

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = @"Indicator to display the delta between fast ATR and ATR.";
                Name = "_ATRDelta";
                Calculate = Calculate.OnBarClose;
                IsOverlay = false;
                DisplayInDataBox = true;
                DrawOnPricePanel = true;
                DrawHorizontalGridLines = true;
                DrawVerticalGridLines = true;
                PaintPriceMarkers = true;
                ScaleJustification = NinjaTrader.Gui.Chart.ScaleJustification.Right;
                IsSuspendedWhileInactive = true;

                FastPeriod = 3;
                Period = 10;
                OverridePlotColors = false;
                CustomPositiveColor = Brushes.DarkGreen;
                CustomNegativeColor = Brushes.DarkRed;

                AddPlot(new Stroke(Brushes.DarkCyan, 2), PlotStyle.Bar, "ATR Delta");
            }
            else if (State == State.DataLoaded)
            {
                _fastATR = ATR(FastPeriod);
                _atr = ATR(Period);
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < Math.Max(Period, FastPeriod))
            {
                ATRDeltaSeries.Reset();
                return;
            }

            double atrDiff = _fastATR[0] - _atr[0];
            ATRDeltaSeries[0] = atrDiff;

            if (OverridePlotColors)
            {
                PlotBrushes[0][0] = (atrDiff >= 0) ? CustomPositiveColor : CustomNegativeColor;
            }
        }
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
    public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
    {
        private ATRDelta[] cacheATRDelta;
        public ATRDelta ATRDelta(int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return ATRDelta(Input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }

        public ATRDelta ATRDelta(ISeries<double> input, int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            if (cacheATRDelta != null)
                for (int idx = 0; idx < cacheATRDelta.Length; idx++)
                    if (cacheATRDelta[idx] != null && cacheATRDelta[idx].FastPeriod == fastPeriod && cacheATRDelta[idx].Period == period && cacheATRDelta[idx].OverridePlotColors == overridePlotColors && cacheATRDelta[idx].CustomPositiveColor == customPositiveColor && cacheATRDelta[idx].CustomNegativeColor == customNegativeColor && cacheATRDelta[idx].EqualsInput(input))
                        return cacheATRDelta[idx];
            return CacheIndicator<ATRDelta>(new ATRDelta() { FastPeriod = fastPeriod, Period = period, OverridePlotColors = overridePlotColors, CustomPositiveColor = customPositiveColor, CustomNegativeColor = customNegativeColor }, input, ref cacheATRDelta);
        }
    }
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
    public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
    {
        public Indicators.ATRDelta ATRDelta(int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.ATRDelta(Input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }

        public Indicators.ATRDelta ATRDelta(ISeries<double> input, int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.ATRDelta(input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }
    }
}

namespace NinjaTrader.NinjaScript.Strategies
{
    public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
        public Indicators.ATRDelta ATRDelta(int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.ATRDelta(Input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }

        public Indicators.ATRDelta ATRDelta(ISeries<double> input, int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.ATRDelta(input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }
    }
}

#endregion
