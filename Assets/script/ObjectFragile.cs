using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFragile : MonoBehaviour
{

    [SerializeField] float CubeSize;
    [SerializeField] float CubesInRow;

    float CubePiviotDistance;
    Vector3 CubePiviot;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] float explosionUpward;

    // Start is called before the first frame update
    void Start()
    {
        CubePiviotDistance = CubeSize * CubesInRow / 2;
        CubePiviot = new Vector3(CubePiviotDistance, CubePiviotDistance, CubePiviotDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "floor")
        {
            Disapear();
        }
    }

    private void Disapear()
    {
        gameObject.SetActive(false);
        for(int x = 0; x < CubesInRow; x++)
        {
            for(int y = 0; y < CubesInRow; y++)
            {
                for(int z = 0; z < CubesInRow; z++)
                {
                    CreatePiece(x, y, z);
                }
            }
        }

        Vector3 ExplodePos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(ExplodePos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
        
    }

    private void CreatePiece(int x, int y, int z)
    {
        GameObject Piece;
        Piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Piece.transform.position = transform.position + new Vector3(CubeSize*x, CubeSize*y, CubeSize*z) - CubePiviot;
        Piece.transform.localScale = new Vector3(CubeSize, CubeSize, CubeSize);
        Piece.AddComponent<Rigidbody>();
        Piece.GetComponent<Rigidbody>().mass = CubeSize;
    }
}
