import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1] / 'JCGAttendanceSystem'


def split_csv(s):
    return [x.strip() for x in s.split(',') if x.strip()]


def test_user_insert_column_value_counts_match():
    text = (ROOT / 'Data' / 'Repositories' / 'UserRepository.cs').read_text(encoding='utf-8')
    m = re.search(r'INSERT\s+INTO\s+Users\s*\((.*?)\)\s*VALUES\s*\((.*?)\)', text, re.I | re.S)
    assert m, 'Users INSERT not found'
    columns = split_csv(m.group(1))
    values = split_csv(m.group(2))
    assert len(columns) == len(values), f'{len(values)} values for {len(columns)} columns: {columns} <- {values}'

if __name__ == '__main__':
    test_user_insert_column_value_counts_match()
    print('PASS')
