using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Tests
{
    /// <summary>
    /// A helper class that uses the Builder Pattern to construct the <see cref="ChartDTO"/>s used in unit tests.
    /// </summary>
    public class ChartDtoBuilder
    {
        private const string BLACK = "Black"; // The default foreground colour.

        private const string WHITE = "White"; // The default background colour.

        private readonly FontDTO font = new FontDTO // The default font.
        {
            Underline = false,
            Family = "Arial",
            Italic = false,
            Bold = false,
            Size = 10,
        };

        private ChartDTO chart; // The DTO being constructed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsBuilder"/> class.
        /// </summary>
        /// <param name="backColour">The background colour.</param>
        /// <param name="foreColour">The foreground colour.</param>
        public ChartDtoBuilder(string backColour, string foreColour)
        {
            chart = new ChartDTO
            {
                BackColour = backColour,
                ForeColour = foreColour,
                Font = font
            };

            CreateAxes();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartDtoBuilder"/> class.
        /// </summary>
        public ChartDtoBuilder()
            : this(WHITE, BLACK) { }

        /// <summary>
        /// Adds a primary x axis with the specified label.
        /// </summary>
        /// <param name="label">The axis label.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="ChartDTO"/>.</returns>
        public ChartDtoBuilder AddX1(string label)
        {
            chart.X1 = new AxisDTO
            {
                Colour = chart.ForeColour,

                Label = new LabelDTO
                {
                    Colour = chart.ForeColour,
                    Visible = true,
                    Text = label,
                    Font = font
                },

                Visible = true
            };

            return this;
        }

        /// <summary>
        /// Adds a secondary x axis with the specified label.
        /// </summary>
        /// <param name="label">The axis label.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="ChartDTO"/>.</returns>
        public ChartDtoBuilder AddX2(string label)
        {
            chart.X2 = new AxisDTO
            {
                Colour = chart.ForeColour,

                Label = new LabelDTO
                {
                    Colour = chart.ForeColour,
                    Visible = true,
                    Text = label,
                    Font = font
                },

                Visible = true
            };

            return this;
        }

        /// <summary>
        /// Adds a primary y axis with the specified label.
        /// </summary>
        /// <param name="label">The axis label.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="ChartDTO"/>.</returns>
        public ChartDtoBuilder AddY1(string label)
        {
            chart.Y1 = new AxisDTO
            {
                Colour = chart.ForeColour,

                Label = new LabelDTO
                {
                    Colour = chart.ForeColour,
                    Visible = true,
                    Text = label,
                    Font = font
                },

                Visible = true
            };

            return this;
        }

        /// <summary>
        /// Adds a secondary y axis with the specified label.
        /// </summary>
        /// <param name="label">The axis label.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="ChartDTO"/>.</returns>
        public ChartDtoBuilder AddY2(string label)
        {
            chart.Y2 = new AxisDTO
            {
                Colour = chart.ForeColour,
                
                Label = new LabelDTO
                {
                    Colour = chart.ForeColour,
                    Visible = true,
                    Text = label,
                    Font = font
                },

                Visible = true
            };

            return this;
        }

        /// <summary>
        /// Adds a title with the specified text.
        /// </summary>
        /// <param name="text">The title text.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="ChartDTO"/>.</returns>
        public ChartDtoBuilder AddTitle(string text)
        {
            chart.Title = new LabelDTO 
            {
                Colour = chart.ForeColour,
                Visible= true,
                Font = font,
                Text = text 
            };

            return this;
        }

        /// <summary>
        /// Creates the <see cref="ChartDTO"/>.
        /// </summary>
        /// <returns>The required <see cref="ChartDTO"/>.</returns>
        public ChartDTO CreateChart()
        {
            return chart;
        }

        /// <summary>
        /// Creates the default axes.
        /// </summary>
        private void CreateAxes()
        {
            chart.X1 = new AxisDTO 
            { 
                Label = new LabelDTO { Font = font },
                Visible = false 
            };

            chart.X2 = new AxisDTO
            {
                Label = new LabelDTO { Font = font },
                Visible = false
            };

            chart.Y1 = new AxisDTO
            {
                Label = new LabelDTO { Font = font },
                Visible = false
            };

            chart.Y2 = new AxisDTO
            {
                Label = new LabelDTO { Font = font },
                Visible = false
            };
        }
    }
}
