﻿using Microsoft.ML.Data;

namespace Well_Up_API.ML.DataModels
{
    public class SamplePrediction
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Score { get; set; }
    }
}



