using UnityEngine;
using MathFunctionParser;

public class MainGrapher : MonoBehaviour {

	

	public string function = "(x *y)";
    public UnityEngine.UI.Text functionInputField;
    private Parser functionParser;
    private Evaluator evaluator;

	[Range(5, 100)]
	public int resolution = 10;

	private int currentResolution;
	private ParticleSystem.Particle[] points;
    void Start() {
        functionParser = new Parser();
        CreatePoints();
        SetPoints(function);
    }

	private void CreatePoints () {
		currentResolution = resolution;
		points = new ParticleSystem.Particle[16* resolution * resolution];
		float increment = 1f / (resolution - 1);
		int i = 0;
		for (int x = (-2 * resolution) ; x < 2*resolution; x++) {
			for (int y = (-2 * resolution) ; y < 2*resolution; y++) {
				Vector3 p = new Vector3(x * increment, 0f, y * increment);
				points[i].position = p;
				points[i++].size = 0.1f;
			}
		}
	}

    public void SetPoints( string function )
    {
        evaluator = functionParser.Parse(function);
        SetPoints();
    }
    public void ParseFunction()
    {
        SetPoints(functionInputField.text);
    }
    private void SetPoints()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p = points[i].position;
            evaluator.SetVariable("x", p.x);
            evaluator.SetVariable("y", p.z);
            p.y = (float)evaluator.Evaluate();
			points[i].position = p;
            points[i].color = new Color(1 - p.y, .5f - p.y, p.y);
        }
		GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    void Update()
    {
        if (currentResolution != resolution || points == null)
        {
            CreatePoints();
            SetPoints();
        }
    }

	private static float Linear (Vector3 p, float t) {
		return p.x;
	}

	private static float Exponential (Vector3 p, float t) {
		return p.x * p.x;
	}

	private static float Parabola (Vector3 p, float t){
		p.x += p.x - 1f;
		p.z += p.z - 1f;
		return 1f - p.x * p.x * p.z * p.z;
	}

	private static float Sine (Vector3 p, float t){
		return 0.50f +
			0.25f * Mathf.Sin(4 * Mathf.PI * p.x + 4 * t) * Mathf.Sin(2 * Mathf.PI * p.z + t) +
			0.10f * Mathf.Cos(3 * Mathf.PI * p.x + 5 * t) * Mathf.Cos(5 * Mathf.PI * p.z + 3 * t) +
			0.15f * Mathf.Sin(Mathf.PI * p.x + 0.6f * t);
	}

	private static float Ripple (Vector3 p, float t){
		p.x -= 0.5f;
		p.z -= 0.5f;
		float squareRadius = p.x * p.x + p.z * p.z;
		return 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius);
	}
}