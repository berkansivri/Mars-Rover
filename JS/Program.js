function Position(x, y, dir) {
  this.x = +x
  this.y = +y
  this.dir = dir
}

const marsRover = function (corner, ...rest) {
  const [maxX, maxY] = corner.split(' ')
  const rovers = []

  for (let i = 0; i < rest.length; i += 2) {

    const [x, y, dir] = rest[i].split(' ')

    rovers.push({
      position: new Position(x, y, dir),
      instructions: rest[i + 1] || ''
    })
  }

  for (const { position, instructions } of rovers) {
    for (const instruction of instructions) {
      if (instruction === 'M') step(position, maxX, maxY)
      else spin(position, instruction)
    }
  }

  return rovers.map(r => Object.values(r.position).join(' '))
}

const spin = (position, direction) => {
  const compass = ['W', 'N', 'E', 'S']
  const currentIndex = compass.indexOf(position.dir)

  if (direction === 'L') {
    position.dir = compass[(currentIndex || compass.length) - 1]
  } else {
    position.dir = compass[(currentIndex + 1) % compass.length]
  }
}

const step = (position, maxX, maxY) => {
  switch (position.dir) {
    case 'W':
      position.x = Math.max(position.x - 1, 0)
      break
    case 'N':
      position.y = Math.min(position.y + 1, maxY)
      break
    case 'E':
      position.x = Math.min(position.x + 1, maxX)
      break
    case 'S':
      position.y = Math.max(position.y - 1, 0)
      break
  }
}

console.log(
  marsRover(
    '5 5',
    '1 2 N',
    'LMLMLMLMM',
    '3 3 E',
    'MMRMMRMRRM'
  )
)