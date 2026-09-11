from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
MAX_RELATIVE_PATH = 100

files = [p for p in ROOT.rglob('*') if p.is_file()]
longest = max(files, key=lambda p: len(str(p.relative_to(ROOT)).replace('\\', '/')))
longest_rel = str(longest.relative_to(ROOT)).replace('\\', '/')
longest_len = len(longest_rel)

assert longest_len <= MAX_RELATIVE_PATH, (
    f'Windows extraction path budget exceeded: {longest_len} chars: {longest_rel}'
)
print(f'PASS: longest relative path is {longest_len} chars: {longest_rel}')
