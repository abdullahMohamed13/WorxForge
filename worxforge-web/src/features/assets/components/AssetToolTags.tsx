interface AssetToolTagsProps {
  tools: string[];
}

export function AssetToolTags({ tools }: AssetToolTagsProps) {
  if (tools.length === 0) {
    return null;
  }

  return (
    <ul className="flex flex-wrap gap-1.5" aria-label="Compatible tools">
      {tools.map((tool) => (
        <li key={tool} className="rounded-full bg-gray-100 px-2.5 py-1 text-xs text-gray-600">
          {tool}
        </li>
      ))}
    </ul>
  );
}
