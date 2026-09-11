from pathlib import Path
import re

root = Path(__file__).resolve().parents[1]
forms = root / 'JCGAttendanceSystem' / 'Forms'
proj = root / 'JCGAttendanceSystem' / 'JCGAttendanceSystem.csproj'
attendance = forms / 'Attendance' / 'AttendanceForm.Designer.cs'

inline = []
for path in forms.rglob('*.Designer.cs'):
    text = path.read_text(encoding='utf-8-sig')
    if re.search(r'Columns\.Add\s*\(\s*new\s+DataGridView(?:TextBox|CheckBox|Button|ComboBox|Image|Link)Column\s*\{', text):
        inline.append(str(path.relative_to(root)))
assert not inline, 'Inline DataGridView column initializers remain: ' + ', '.join(inline)

project_text = proj.read_text(encoding='utf-8-sig')
assert '<Compile Include="Properties\\Resources.Designer.cs">' in project_text, 'Resources.Designer.cs is missing from explicit Compile items'
assert '<AutoGen>True</AutoGen>' in project_text and '<DesignTime>True</DesignTime>' in project_text, 'Resources.Designer.cs missing design-time metadata'

text = attendance.read_text(encoding='utf-8-sig')
row = float(re.search(r'RowStyles\.Add\(new RowStyle\(SizeType\.Absolute,\s*([0-9.]+)F\)\)', text).group(1))
margin_bottom = int(re.search(r'_searchCard\.Margin\s*=\s*new Padding\([^,]+,[^,]+,[^,]+,\s*(\d+)\)', text).group(1))
search_y = int(re.search(r'_search\.Location\s*=\s*new Point\([^,]+,\s*(\d+)\)', text).group(1))
search_h = int(re.search(r'_search\.Size\s*=\s*new Size\([^,]+,\s*(\d+)\)', text).group(1))
button_y = int(re.search(r'_searchButton\.Location\s*=\s*new Point\([^,]+,\s*(\d+)\)', text).group(1))
button_h = int(re.search(r'_searchButton\.Size\s*=\s*new Size\([^,]+,\s*(\d+)\)', text).group(1))
effective = row - margin_bottom
assert search_y + search_h <= effective, f'Search TextBox clipped: bottom={search_y+search_h}, usable={effective}'
assert button_y + button_h <= effective, f'Search button clipped: bottom={button_y+button_h}, usable={effective}'
print('designer/runtime compatibility checks passed')
