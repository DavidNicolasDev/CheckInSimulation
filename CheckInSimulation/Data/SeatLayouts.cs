using CheckInSimulation.Models;

namespace CheckInSimulation.Data
{
    public class SeatLayouts
    {
        public static List<SeatLayout> Layouts = new List<SeatLayout>
        {
            // AirNova-660
            new SeatLayout
            {
                AirplaneId = 1,
                Classes = new Dictionary<int, SeatConfig>
                {

                    // Primera clase (2-2)
                    {
                        1, new SeatConfig
                        {
                            Rows = new List<int> {1, 2, 3, 4},
                            ColumnGroups = new List<List<string>>
                            {
                                new List<string> {"A", "B"}, // lado izquierdo
                                new List<string> {"F", "G"}  // lado derecho
                            }
                        }
                    },
                    // Económica Premium (3-3)
                    {
                        2, new SeatConfig
                        {
                            Rows = new List<int> {8, 9, 10, 11, 12, 13, 14, 15},
                            ColumnGroups = new List<List<string>>
                            {
                                new List<string> {"A", "B", "C"}, // lado izquierdo
                                new List<string> {"E", "F", "G"}  // lado derecho
                            }
                        }
                    },
                    // Económica (3-3)
                    {
                        3, new SeatConfig
                        {
                            Rows = new List<int> { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34},
                            ColumnGroups = new List<List<string>>
                            {
                                new List<string> {"A", "B", "C"}, // lado izquierdo
                                new List<string> {"E", "F", "G"}  // lado derecho
                            }
                        }
                    }


                }
            },
            // AirMax-720neo
            new SeatLayout
            {
                AirplaneId = 2,
                Classes = new Dictionary<int, SeatConfig>
                {
                    // Primera clase (1-1-1)
                    {
                        1, new SeatConfig
                        {
                            Rows = new List<int> {1, 2, 3, 4, 5},
                            ColumnGroups = new List<List<string>>
                            {
                                new List<string> {"A"},       // lado izquierdo
                                new List<string> {"E"},       // centro
                                new List<string> {"I"}        // lado derecho
                            }
                        }
                    },
                    // Económica Premium (2-3-2)
                    {
                        2, new SeatConfig
                        {
                            Rows = new List<int> {9, 10, 11, 12, 13, 14},
                            ColumnGroups = new List<List<string>>
                            {
                                new List<string> {"A", "B"},       // lado izquierdo
                                new List<string> {"D", "E", "F"},  // centro
                                new List<string> {"H", "I"}        // lado derecho
                            }
                        }
                    },
                    // Económica (2-3-2)
                    {
                        3, new SeatConfig
                        {
                            Rows = new List<int> {18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31},
                            ColumnGroups = new List<List<string>>
                            {
                                new List<string> {"A", "B"},       // lado izquierdo
                                new List<string> {"D", "E", "F"},  // centro
                                new List<string> {"H", "I"}        // lado derecho
                            }
                        }
                    }
                }
            }
        };
    }
}
