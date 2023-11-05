using System;
using Microsoft.Extensions.ML;
using Microsoft.AspNetCore.Mvc;
using Well_Up_API.ML.DataModels;

namespace Well_Up_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentimentController : ControllerBase
    {
        private readonly PredictionEnginePool<SampleObservation, SamplePrediction> _predictionEnginePool;

        public SentimentController(PredictionEnginePool<SampleObservation, SamplePrediction> predictionEnginePool)
        {
            // Get the ML Model Engine injected, for scoring
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpGet]
        [Route("sentimentprediction")]
        public ActionResult<float> PredictSentiment([FromQuery] string sentimentText)
        {
            // Predict sentiment using ML.NET model
            SampleObservation sampleData = new SampleObservation { Col0 = sentimentText };

            // Predict sentiment
            SamplePrediction prediction = _predictionEnginePool.Predict(sampleData);

            float percentage = CalculatePercentage(prediction.Score);

            return percentage;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public int CalculatePercentage(double value)
        {
            return (int)Math.Round(10 * (1.0f / (1.0f + (float)Math.Exp(-value))));
        }
    }
}