using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaylexStudios.Platform {
    public class Platform : MonoBehaviour {
        [SerializeField] float speed = 1f;
        [Range(-1, 1)] [SerializeField] int direction = 1;
        [SerializeField] bool infiniteScroll = false;
        [SerializeField] bool vertical = false;
        [SerializeField] float max;
        [SerializeField] float min;

        Vector2 horizontalMove;
        Vector2 verticalMove;

        int type;

        void Start() {
            type = DetermineType();
        }

        int DetermineType() {
            if (!infiniteScroll && !vertical) {
                return 1;
            } else if (infiniteScroll && !vertical) {
                return 2;
            } else if (!infiniteScroll && vertical) {
                return 3;
            } else /*if (infiniteScroll && vertical)*/ {
                return 4;
            }
        }

        void FixedUpdate() {
            verticalMove = new Vector2(transform.position.x, transform.position.y + direction * speed * Time.deltaTime);
            horizontalMove = new Vector2(transform.position.x + direction * speed * Time.deltaTime, transform.position.y);

            switch (type) {
                case 1:
                    HorizontalMove();
                    break;
                case 2:
                    HorizontalScroll();
                    break;
                case 3:
                    VerticalMove();
                    break;
                case 4:
                    VerticalScroll();
                    break;
                default:
                    break;
            }
        }

        void HorizontalMove() {
            transform.position = horizontalMove;

            if (max < transform.position.x || min > transform.position.x) {
                direction = -direction;
            }
        }

        void HorizontalScroll() {
            transform.position = horizontalMove;

            if (max < transform.position.x && direction == 1) {
                transform.position = new Vector2(min, transform.position.y);
            } else if (min > transform.position.x && direction == -1) {
                transform.position = new Vector2(max, transform.position.y);
            }
        }

        void VerticalMove() {
            transform.position = verticalMove;

            if (max < transform.position.y || min > transform.position.y) {
                direction = -direction;
            }
        }

        void VerticalScroll() {
            transform.position = verticalMove;

            if (max < transform.position.y && direction == 1) {
                transform.position = new Vector2(transform.position.x, min);
            } else if (min > transform.position.y && direction == -1) {
                transform.position = new Vector2(transform.position.x, max);
            }
        }

        void OnDrawGizmos() {
            if (!infiniteScroll && !vertical) {
                Gizmos.DrawLine(new Vector3(min, transform.position.y, 0), new Vector3(max, transform.position.y, 0));

                for (int i = 0; i < (max - min + 1) / 2; i++) {
                    Gizmos.DrawLine(new Vector3(max - i - 0.25f, transform.position.y + 0.25f, 0), new Vector3(max - i, transform.position.y, 0));
                    Gizmos.DrawLine(new Vector3(max - i - 0.25f, transform.position.y - 0.25f, 0), new Vector3(max - i, transform.position.y, 0));
                    Gizmos.DrawLine(new Vector3(min + i + 0.25f, transform.position.y + 0.25f, 0), new Vector3(min + i, transform.position.y, 0));
                    Gizmos.DrawLine(new Vector3(min + i + 0.25f, transform.position.y - 0.25f, 0), new Vector3(min + i, transform.position.y, 0));
                }
            } else if (infiniteScroll && !vertical) {
                Gizmos.DrawLine(new Vector3(min, transform.position.y, 0), new Vector3(max, transform.position.y, 0));

                for (int i = 0; i < max - min + 1; i++) {
                    Gizmos.DrawLine(new Vector3(max - i - 0.25f * direction, transform.position.y + 0.25f, 0), new Vector3(max - i, transform.position.y, 0));
                    Gizmos.DrawLine(new Vector3(max - i - 0.25f * direction, transform.position.y - 0.25f, 0), new Vector3(max - i, transform.position.y, 0));
                }
            } else if (!infiniteScroll && vertical) {
                Gizmos.DrawLine(new Vector3(transform.position.x, min, 0), new Vector3(transform.position.x, max, 0));

                for (int i = 0; i < (max - min + 1) / 2; i++) {
                    Gizmos.DrawLine(new Vector3(transform.position.x + 0.25f, max - i - 0.25f, 0), new Vector3(transform.position.x, max - i, 0));
                    Gizmos.DrawLine(new Vector3(transform.position.x - 0.25f, max - i - 0.25f, 0), new Vector3(transform.position.x, max - i, 0));
                    Gizmos.DrawLine(new Vector3(transform.position.x + 0.25f, min + i + 0.25f, 0), new Vector3(transform.position.x, min + i, 0));
                    Gizmos.DrawLine(new Vector3(transform.position.x - 0.25f, min + i + 0.25f, 0), new Vector3(transform.position.x, min + i, 0));
                }
            } else /*if (infiniteScroll && vertical)*/ {
                Gizmos.DrawLine(new Vector3(transform.position.x, min, 0), new Vector3(transform.position.x, max, 0));

                for (int i = 0; i < max - min + 1; i++) {
                    Gizmos.DrawLine(new Vector3(transform.position.x + 0.25f, max - i - 0.25f * direction, 0), new Vector3(transform.position.x, max - i, 0));
                    Gizmos.DrawLine(new Vector3(transform.position.x - 0.25f, max - i - 0.25f * direction, 0), new Vector3(transform.position.x, max - i, 0));
                }
            }
        }
    }
}

