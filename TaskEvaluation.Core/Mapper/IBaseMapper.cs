namespace TaskEvaluation.Core.Mapper
{
	public interface IBaseMapper<TSource, TDestination>
	{
		TDestination MapModel(TSource source);
		IEnumerable<TDestination> MapList(IEnumerable<TSource> source);
	}
}
