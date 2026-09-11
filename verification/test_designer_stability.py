from pathlib import Path
import re, sys
root=Path('/mnt/data/JCG-Attendance-System-v3.1.2-Designer-Stability/JCGAttendanceSystem/Forms')
problems=[]
for f in sorted(root.rglob('*Form.cs')):
    if f.name.endswith('.Designer.cs'): continue
    text=f.read_text(encoding='utf-8')
    name=f.stem
    if not re.search(rf'public\s+partial\s+class\s+{re.escape(name)}\s*:\s*Form', text):
        problems.append(f'{f.relative_to(root)}: root form is not declared public partial class {name} : Form')
    if re.search(r'private\s+readonly\s+\w*Service\s+_\w+\s*=\s*new\s+\w*Service\s*\(', text):
        problems.append(f'{f.relative_to(root)}: service constructed in field initializer')
    if re.search(rf'public\s+{re.escape(name)}\s*\(\s*\)\s*:\s*this\s*\(', text):
        problems.append(f'{f.relative_to(root)}: parameterless constructor chains into runtime constructor')
    m=re.search(rf'public\s+{re.escape(name)}\s*\(\s*\)\s*(?:\{{|\n\s*\{{)(.*?)\n\s*\}}', text, re.S)
    if m and 'InitializeComponent();' not in m.group(1):
        problems.append(f'{f.relative_to(root)}: parameterless constructor does not directly call InitializeComponent')
if problems:
    print('FAIL')
    for p in problems: print(' -',p)
    sys.exit(1)
print('PASS: all forms follow design-time-safe root/constructor conventions')
