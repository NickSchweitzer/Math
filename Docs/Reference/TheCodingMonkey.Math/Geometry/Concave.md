# Geometry.Concave method (1 of 2)

Determines whether a polygon is considered Concave, where at least one interior angle is between 180 and 360 degrees.

```csharp
public static bool? Concave(this IList<Point> points)
```

| parameter | description |
| --- | --- |
| points | List of points which define the boundaries of the polygon |

## Return Value

True if the polygon is Concave, false if it is Convex, and null if it cannot be computed.

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if the IList of points contains fewer than three elements. |

## Remarks

The polygon is assumed to be closed, with the last point in the array not being the same as the first point in the array.

## See Also

* class [Geometry](../Geometry.md)
* namespace [TheCodingMonkey.Math](../../TheCodingMonkey.Math.md)

---

# Geometry.Concave method (2 of 2)

Determines whether a polygon is considered Concave, where at least one interior angle is between 180 and 360 degrees.

```csharp
public static bool? Concave(this IList<PointF> points)
```

| parameter | description |
| --- | --- |
| points | List of points which define the boundaries of the polygon |

## Return Value

True if the polygon is Concave, false if it is Convex, and null if it cannot be computed.

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if the IList of points contains fewer than three elements. |

## Remarks

The polygon is assumed to be closed, with the last point in the array not being the same as the first point in the array.

## See Also

* class [Geometry](../Geometry.md)
* namespace [TheCodingMonkey.Math](../../TheCodingMonkey.Math.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Math.dll -->
